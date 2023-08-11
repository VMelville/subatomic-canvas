using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class TimeViewFacade : IStartable
    {
        [Inject] private TimeView _view;
        
        [Inject] private TimeManager _timeManager;
        
        public void Start()
        {
            _timeManager.NowTime.Subscribe(_view.SetTime);
            _timeManager.Speed.Subscribe(_view.SetSpeed);
        }
    }
}