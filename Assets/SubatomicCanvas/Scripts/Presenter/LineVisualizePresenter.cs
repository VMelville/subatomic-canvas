using System.Collections.Generic;
using ParticleSim.Result;
using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class LineVisualizePresenter : IStartable
    {
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private LineVisualizeView _lineVisualizeView;
        
        public void Start()
        {
            _lastSimulationCondition.result.Subscribe(OnCompletedSimulation);
        }
        
        private void OnCompletedSimulation((SimulationResult, Dictionary<string, (int, int)>) result)
        {
            var (simulationResult, _) = result;
            
            if (simulationResult == null) return;
            
            _lineVisualizeView.ClearLine();
            _lineVisualizeView.DrawLine(simulationResult.Trajectories);
        }
    }
}