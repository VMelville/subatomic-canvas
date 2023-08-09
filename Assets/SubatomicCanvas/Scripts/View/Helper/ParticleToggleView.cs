using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class ParticleToggleView : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Text mainText;
        [SerializeField] private Text subText;

        public Toggle.ToggleEvent OnValueChanged => toggle.onValueChanged;

        public void SetIsOn(bool isOn)
        {
            toggle.isOn = isOn;
        }
        
        public bool GetIsOn()
        {
            return toggle.isOn;
        }

        public void SetMainText(string text)
        {
            mainText.text = text;
        }
        
        public void SetSubText(string text)
        {
            subText.text = text;
        }
    }
}