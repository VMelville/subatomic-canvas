using SubatomicCanvas.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SpeedSliderView : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        [SerializeField] private GameObject fastForwardIcon;
        [SerializeField] private GameObject playIcon;
        [SerializeField] private GameObject pauseIcon;
        [SerializeField] private GameObject reverseIcon;
        [SerializeField] private GameObject fastRewindIcon;

        public UnityEvent<float> onValueChanged => slider.onValueChanged;
        
        private const float Threshold = 1.5f;
        
        private void Start()
        {
            Debug.LogWarning("ToDo: スライダーのハンドル画像をスライダー位置によって変える");
        }

        public void SetSpeed(float speed)
        {
            fastForwardIcon.SetActive(speed > Threshold);
            playIcon       .SetActive(speed is > 0.0f and <= Threshold);
            pauseIcon      .SetActive(speed == 0.0f);
            reverseIcon    .SetActive(speed is < 0.0f and >= -Threshold);
            fastRewindIcon .SetActive(speed < -Threshold);
            
        }
    }
}
