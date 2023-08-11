using SubatomicCanvas.Model;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class LastSimulationConditionManagerFacade : IStartable
    {
        [Inject] private LastSimulationConditionManager _manager;
        
        [Inject] private SimulationService _simulationService;

        public void Start()
        {
            _simulationService.OnSimulationCompleted.AddListener(_manager.SetResult);
        }
    }
}