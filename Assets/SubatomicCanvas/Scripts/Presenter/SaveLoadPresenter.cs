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
        [Inject] private SaveLoadState _saveLoadState;
        [Inject] private CanvasState _canvasState;
        
        // View
        [Inject] private SaveLoadView _saveLoadView;
        
        public void Start()
        {
            _saveLoadState.CanvasDataFiles.ObserveAdd().Subscribe(addEvent =>
            {
                var filePath = addEvent.Key;
                var fileInfo = addEvent.Value;
                var isActive = fileInfo.title == _saveLoadState.FileNameCandidate.Value;
                var isDisplayTrashButton = _saveLoadState.IsDisplayTrashButton.Value;
                _saveLoadView.AddDataContent(filePath, fileInfo, isActive, isDisplayTrashButton);
            });

            _saveLoadState.CanvasDataFiles.ObserveReplace().Subscribe(replaceEvent =>
            {
                var filePath = replaceEvent.Key;
                var fileInfo = replaceEvent.NewValue;
                _saveLoadView.ReplaceDataContent(filePath, fileInfo);
            });
            
            _saveLoadState.CanvasDataFiles.ObserveReset().Subscribe(_ => _saveLoadView.ClearDataContent());
            _saveLoadState.CanvasDataFiles.ObserveRemove().Subscribe(removeEvent => _saveLoadView.RemoveDataContent(removeEvent.Key, removeEvent.Value));
            _saveLoadState.IsDisplayTrashButton.Subscribe(_saveLoadView.DisplayTrashButton);
            _saveLoadState.FileNameCandidate.Subscribe(_saveLoadView.ChangeFileNameCandidate);
            
            _saveLoadView.OnClickDisplayTrashButton.AddListener(_saveLoadState.ToggleDisplayTrashButton);
            _saveLoadView.OnClickReloadButton.AddListener(_saveLoadState.ReloadFiles);
            _saveLoadView.OnClickSaveButton.AddListener(() => _saveLoadState.SaveFile(_canvasState));
            _saveLoadView.OnChangeFileName.AddListener(_saveLoadState.SetFileNameCandidate);
            _saveLoadView.OnClickLoadFileButton.AddListener(_canvasState.LoadFile);
            _saveLoadView.OnClickTrashFileButton.AddListener(_saveLoadState.DeleteFile);
            _saveLoadView.OnClickView.AddListener(_saveLoadState.SetFileNameCandidate);
            
            _saveLoadState.ReloadFiles();
        }
    }
}