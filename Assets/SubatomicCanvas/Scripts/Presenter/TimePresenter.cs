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
            
            Debug.LogWarning("ToDo: 時間操作系の入力を受けてTimeStateに渡す");
        }

        // ToDo: 時間操作はPresenterの範疇を超えていそうです。
        public void Tick()
        {
            _timeState.time.Value += _timeState.speed.Value * Time.deltaTime;
        }
    }
}