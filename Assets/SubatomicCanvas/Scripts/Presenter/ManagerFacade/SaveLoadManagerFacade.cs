using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SaveLoadManagerFacade : IStartable
    {
        [Inject] private SaveLoadManager _manager;
        
        [Inject] private CanvasManager _canvasManager;
        [Inject] private SaveLoadView _saveLoadView;
        
        public void Start()
        {
            _saveLoadView.OnClickDisplayTrashButton.AddListener(_manager.ToggleDisplayTrashButton);
            _saveLoadView.OnClickReloadButton.AddListener(_manager.ReloadFiles);
            _saveLoadView.OnClickSaveButton.AddListener(() => _manager.SaveFile(_canvasManager));
            _saveLoadView.OnChangeFileName.AddListener(_manager.SetFileNameCandidate);
            _saveLoadView.OnClickLoadFileButton.AddListener(_canvasManager.LoadFile);
            _saveLoadView.OnClickTrashFileButton.AddListener(_manager.DeleteFile);
            _saveLoadView.OnClickView.AddListener(_manager.SetFileNameCandidate);
            
            _manager.ReloadFiles();
        }
    }
}