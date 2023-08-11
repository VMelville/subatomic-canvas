using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class TimePresenter : IStartable
    {
        // Model
        [Inject] private TimeManager _timeManager;
        
        // View
        [Inject] private TimeView _timeView;

        public void Start()
        {
            _timeManager.NowTime.Subscribe(_timeView.SetTime);
            _timeManager.Speed.Subscribe(_timeView.SetSpeed);

            _timeView.OnSpeedChanged.AddListener(_timeManager.SetSpeed);
        }
    }
}