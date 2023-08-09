using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private SpeedSliderView speedSliderView;
        [SerializeField] private TMP_Text timeText;

        public Slider.SliderEvent OnSpeedChanged => speedSliderView.OnValueChanged;

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