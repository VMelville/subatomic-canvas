using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class TimeManagerFacade : IStartable, ITickable
    {
        [Inject] private TimeManager _manager;

        [Inject] private TimeView _timeView;
        [Inject] private SimulationService _simulationService;

        public void Start()
        {
            _timeView.OnSpeedChanged.AddListener(_manager.SetSpeed);
            _simulationService.OnSimulationCompleted.AddListener((_, _, _) => _manager.OnSimulationCompleted());
        }
        
        public void Tick() => _manager.Tick();
    }
}