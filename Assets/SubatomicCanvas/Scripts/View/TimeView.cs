using SubatomicCanvas.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SubatomicCanvas.View
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private SpeedSliderView speedSliderView;
        [SerializeField] private TMP_Text timeText;

        public UnityEvent<float> onSpeedChanged => speedSliderView.onValueChanged;

        public void SetTime(float time)
        {
            timeText.text = time.ToString("F2") + " ns";
        }

        public void SetSpeed(float speed)
        {
            speedSliderView.SetSpeed(speed);
        }
    }
}