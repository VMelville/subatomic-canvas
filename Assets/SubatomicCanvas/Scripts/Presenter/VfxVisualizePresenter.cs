using System.Collections.Generic;
using ParticleSim.Result;
using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class VfxVisualizePresenter : IStartable
    {
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private VfxVisualizeView _vfxVisualizeView;
        [Inject] private TimeState _timeState;

        public void Start()
        {
            _lastSimulationCondition.result.Subscribe(OnCompletedSimulation);
            _timeState.time.Subscribe(_vfxVisualizeView.OnPassesTime);
        }
        
        private void OnCompletedSimulation((SimulationResult, Dictionary<string, (int, int)>) result)
        {
            var (simulationResult, _) = result;
            
            if (simulationResult == null) return;

            _vfxVisualizeView.PlayVfx(simulationResult.LinearTrajectory);
        }
    }
}