using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    // ToDo: もっとシンプルに制御できるような気もします。。。
    public class UiVisibleViewFacade : IStartable
    {
        [Inject] private UiVisibleView _view;
        
        [Inject] private SnapshotManager _snapshotManager;
        
        public void Start() =>
            _snapshotManager.State
                .Where(state => state is SnapshotStateType.PrePare or SnapshotStateType.Took)
                .Select(state => state == SnapshotStateType.Took)
                .Subscribe(_view.SetIsVisible);
    }
}