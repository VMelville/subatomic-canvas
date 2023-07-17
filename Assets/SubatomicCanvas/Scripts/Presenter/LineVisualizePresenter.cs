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
        
        private void OnCompletedSimulation(SimulationResult result)
        {
            if (result == null) return;
            
            _lineVisualizeView.ClearLine();
            _lineVisualizeView.DrawLine(result.Trajectories);
        }
    }
}