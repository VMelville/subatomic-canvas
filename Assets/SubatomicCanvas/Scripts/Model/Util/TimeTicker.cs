using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Model
{
    public class TimeTicker : ITickable
    {
        [Inject] private TimeManager _manager;
        
        public void Tick()
        {
            _manager.Tick();
        }
    }
}