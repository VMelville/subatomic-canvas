using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SaveLoadPresenter : IStartable
    {
        // Model
        [Inject] private SaveLoadManager _saveLoadManager;
        [Inject] private CanvasManager _canvasManager;
        
        // View
        [Inject] private SaveLoadView _saveLoadView;
        
        public void Start()
        {
            _saveLoadManager.CanvasDataFiles.ObserveAdd().Subscribe(addEvent =>
            {
                var filePath = addEvent.Key;
                var fileInfo = addEvent.Value;
                var isActive = fileInfo.title == _saveLoadManager.FileNameCandidate.Value;
                var isDisplayTrashButton = _saveLoadManager.IsDisplayTrashButton.Value;
                _saveLoadView.AddDataContent(filePath, fileInfo, isActive, isDisplayTrashButton);
            });

            _saveLoadManager.CanvasDataFiles.ObserveReplace().Subscribe(replaceEvent =>
            {
                var filePath = replaceEvent.Key;
                var fileInfo = replaceEvent.NewValue;
                _saveLoadView.ReplaceDataContent(filePath, fileInfo);
            });
            
            _saveLoadManager.CanvasDataFiles.ObserveReset().Subscribe(_ => _saveLoadView.ClearDataContent());
            _saveLoadManager.CanvasDataFiles.ObserveRemove().Subscribe(removeEvent => _saveLoadView.RemoveDataContent(removeEvent.Key, removeEvent.Value));
            _saveLoadManager.IsDisplayTrashButton.Subscribe(_saveLoadView.DisplayTrashButton);
            _saveLoadManager.FileNameCandidate.Subscribe(_saveLoadView.ChangeFileNameCandidate);
            
            _saveLoadView.OnClickDisplayTrashButton.AddListener(_saveLoadManager.ToggleDisplayTrashButton);
            _saveLoadView.OnClickReloadButton.AddListener(_saveLoadManager.ReloadFiles);
            _saveLoadView.OnClickSaveButton.AddListener(() => _saveLoadManager.SaveFile(_canvasManager));
            _saveLoadView.OnChangeFileName.AddListener(_saveLoadManager.SetFileNameCandidate);
            _saveLoadView.OnClickLoadFileButton.AddListener(_canvasManager.LoadFile);
            _saveLoadView.OnClickTrashFileButton.AddListener(_saveLoadManager.DeleteFile);
            _saveLoadView.OnClickView.AddListener(_saveLoadManager.SetFileNameCandidate);
            
            _saveLoadManager.ReloadFiles();
        }
    }
}