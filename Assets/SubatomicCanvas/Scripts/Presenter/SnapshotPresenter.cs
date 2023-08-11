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
        [Inject] private SnapshotState _snapshotState;
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private TimeState _timeState;
        
        // Model - Service
        [Inject] private SnapshotService _snapshotService;
        
        // View
        [Inject] private UiVisibleView _uiVisibleView;
        [Inject] private SnapshotButtonView _snapshotButtonView;
        
        public void Start()
        {
            _snapshotState.State.Subscribe(state =>
            {
                switch (state)
                {
                    case SnapshotStateType.NormalTime:
                        break;
                    case SnapshotStateType.PrePare:
                        _uiVisibleView.SetIsVisible(false);
                        break;
                    case SnapshotStateType.Standby:
                        _snapshotService.TakeSnapshot(_lastSimulationCondition.ParticleKey.Value,
                            _timeState.NowTime.Value);
                        break;
                    case SnapshotStateType.Took:
                        _uiVisibleView.SetIsVisible(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            });
            
            _snapshotButtonView.OnClick.AddListener(_snapshotState.DoSnapshot);
            _uiVisibleView.onSetActive.AddListener(_snapshotState.OnSetActiveUi);
            _snapshotService.onTookSnapshot.AddListener(_snapshotState.OnTookSnapshot);
        }
    }
}