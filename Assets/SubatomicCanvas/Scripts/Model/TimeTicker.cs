using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Model
{
    public class TimeTicker : ITickable
    {
        [Inject] private TimeState _state;
        
        public void Tick()
        {
            _state.Tick();
        }
    }
}