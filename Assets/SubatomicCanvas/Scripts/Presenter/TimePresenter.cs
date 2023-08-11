using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class TimePresenter : IStartable
    {
        [Inject] private TimeState _timeState;
        [Inject] private TimeView _timeView;

        public void Start()
        {
            _timeState.NowTime.Subscribe(_timeView.SetTime);
            _timeState.Speed.Subscribe(_timeView.SetSpeed);

            _timeView.OnSpeedChanged.AddListener(_timeState.SetSpeed);
        }
    }
}