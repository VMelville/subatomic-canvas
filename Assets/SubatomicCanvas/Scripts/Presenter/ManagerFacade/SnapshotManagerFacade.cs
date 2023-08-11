using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    // ToDo: もっとシンプルに制御できるような気もします。。。
    public class SnapshotManagerFacade : IStartable
    {
        [Inject] private SnapshotManager _manager;
        
        [Inject] private SnapshotService _snapshotService;
        [Inject] private UiVisibleView _uiVisibleView;
        [Inject] private SnapshotButtonView _snapshotButtonView;
        
        public void Start()
        {
            _snapshotButtonView.OnClick.AddListener(_manager.DoSnapshot);
            _uiVisibleView.onSetActive.AddListener(_manager.OnSetActiveUi);
            _snapshotService.onTookSnapshot.AddListener(_manager.OnTookSnapshot);
        }
    }
}