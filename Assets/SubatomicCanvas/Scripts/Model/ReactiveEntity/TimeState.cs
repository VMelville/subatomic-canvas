using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public class TimeState
    {
        public IReadOnlyReactiveProperty<float> NowTime => _nowTime;
        public IReadOnlyReactiveProperty<float> Speed => _speed;

        private readonly FloatReactiveProperty _nowTime = new();
        private readonly FloatReactiveProperty _speed = new(0f);

        public void Tick()
        {
            _nowTime.Value = Mathf.Max(_nowTime.Value + _speed.Value * Time.deltaTime, 0.0f);
        }
        
        public void OnSimulationCompleted()
        {
            _nowTime.Value = 0.0f;
            
            // 速度が0以下であれば、強制的に0.1に変更する
            if (_speed.Value <= 0)
            {
                SetSpeed(0.1f);
            }
        }

        public void SetSpeed(float speed)
        {
            _speed.Value = speed;
        }
    }
}