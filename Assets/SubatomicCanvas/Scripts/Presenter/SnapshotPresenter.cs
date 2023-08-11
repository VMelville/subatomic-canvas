using System;
using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    // ToDo: もっとシンプルに制御できるような気もします。。。
    public class SnapshotPresenter : IStartable
    {
        // Model - ReactiveEntity
        [Inject] private SnapshotManager _snapshotManager;
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private TimeManager _timeManager;
        
        // Model - Service
        [Inject] private SnapshotService _snapshotService;
        
        // View
        [Inject] private UiVisibleView _uiVisibleView;
        [Inject] private SnapshotButtonView _snapshotButtonView;
        
        public void Start()
        {
            _snapshotManager.State.Subscribe(state =>
            {
                switch (state)
                {
                    case SnapshotStateType.NormalTime:
                        break;
                    case SnapshotStateType.PrePare:
                        _uiVisibleView.SetIsVisible(false);
                        break;
                    case SnapshotStateType.Standby:
                        _snapshotService.TakeSnapshot(_lastSimulationConditionManager.ParticleKey.Value,
                            _timeManager.NowTime.Value);
                        break;
                    case SnapshotStateType.Took:
                        _uiVisibleView.SetIsVisible(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            });
            
            _snapshotButtonView.OnClick.AddListener(_snapshotManager.DoSnapshot);
            _uiVisibleView.onSetActive.AddListener(_snapshotManager.OnSetActiveUi);
            _snapshotService.onTookSnapshot.AddListener(_snapshotManager.OnTookSnapshot);
        }
    }
}