using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class UiVisibleViewFacade : ControllerBase, IStartable
    {
        [Inject] private UiVisibleView _view;
        
        [Inject] private SnapshotManager _snapshotManager;
        
        public void Start() =>
            _snapshotManager.State
                .Where(state => state is SnapshotStateType.PrePare or SnapshotStateType.Took)
                .Select(state => state == SnapshotStateType.Took)
                .Subscribe(_view.SetIsVisible)
                .AddTo(this);
    }
}