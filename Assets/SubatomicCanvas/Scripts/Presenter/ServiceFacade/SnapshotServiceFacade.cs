﻿using SubatomicCanvas.Model;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SnapshotServiceFacade : ControllerBase, IStartable
    {
        [Inject] private SnapshotService _service;
        
        [Inject] private SnapshotManager _snapshotManager;
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private TimeManager _timeManager;

        public void Start()
        {
            _snapshotManager.State
                .Where(state => state == SnapshotStateType.Standby)
                .Subscribe(_ => _service.TakeSnapshot(_lastSimulationConditionManager.ParticleKey.Value, _timeManager.NowTime.Value))
                .AddTo(this);
        }
    }
}