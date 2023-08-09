using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class TimePresenter : IStartable, ITickable
    {
        [Inject] private TimeState _timeState;
        [Inject] private TimeView _timeView;

        public void Start()
        {
            _timeState.time.Subscribe(_timeView.SetTime);
            _timeState.speed.Subscribe(_timeView.SetSpeed);

            _timeView.OnSpeedChanged.AddListener(OnSpeedChanged);
        }

        private void OnSpeedChanged(float speed)
        {
            _timeState.speed.Value = speed;
        }
        
        // ToDo: 時間操作はPresenterの範疇を超えているかも…
        public void Tick()
        {
            var time = _timeState.time.Value;
            var speed = _timeState.speed.Value;
            
            _timeState.time.Value = Mathf.Max(time + speed * Time.deltaTime, 0.0f);
        }
    }
}