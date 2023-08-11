using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SaveLoadViewFacade : IInitializable
    {
        [Inject] private SaveLoadView _view;
        
        [Inject] private SaveLoadManager _saveLoadManager;
        [Inject] private CanvasManager _canvasManager;
        
        public void Initialize()
        {
            _saveLoadManager.CanvasDataFiles.ObserveAdd().Subscribe(addEvent =>
            {
                var filePath = addEvent.Key;
                var fileInfo = addEvent.Value;
                var isActive = fileInfo.title == _saveLoadManager.FileNameCandidate.Value;
                var isDisplayTrashButton = _saveLoadManager.IsDisplayTrashButton.Value;
                _view.AddDataContent(filePath, fileInfo, isActive, isDisplayTrashButton);
            });

            _saveLoadManager.CanvasDataFiles.ObserveReplace().Subscribe(replaceEvent =>
            {
                var filePath = replaceEvent.Key;
                var fileInfo = replaceEvent.NewValue;
                _view.ReplaceDataContent(filePath, fileInfo);
            });
            
            _saveLoadManager.CanvasDataFiles.ObserveReset().Subscribe(_ => _view.ClearDataContent());
            _saveLoadManager.CanvasDataFiles.ObserveRemove().Subscribe(removeEvent => _view.RemoveDataContent(removeEvent.Key, removeEvent.Value));
            _saveLoadManager.IsDisplayTrashButton.Subscribe(_view.DisplayTrashButton);
            _saveLoadManager.FileNameCandidate.Subscribe(_view.ChangeFileNameCandidate);
        }
    }
}