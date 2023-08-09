using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SpeedSliderView : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        [SerializeField] private Image icon;
        
        [SerializeField] private Sprite fastForwardSprite;
        [SerializeField] private Sprite playSprite;
        [SerializeField] private Sprite pauseSprite;
        [SerializeField] private Sprite reverseSprite;
        [SerializeField] private Sprite fastRewindSprite;

        public Slider.SliderEvent OnValueChanged => slider.onValueChanged;
        
        private const float Threshold = 1.5f;
        
        public void SetSpeed(float speed)
        {
            icon.sprite = speed switch
            {
                > Threshold              => fastForwardSprite,
                > 0.0f and <= Threshold  => playSprite,
                0.0f                     => pauseSprite,
                < 0.0f and >= -Threshold => reverseSprite,
                < -Threshold             => fastRewindSprite,
                _                        => icon.sprite
            };
        }
    }
}
