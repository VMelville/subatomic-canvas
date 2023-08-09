using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SimulatorView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text simulatorText;

        public Button.ButtonClickedEvent OnClick => button.onClick;

        public void SetText(string text)
        {
            simulatorText.text = text;
        }

        public void SetDisplayParticleName(bool isDisplayParticleName)
        {
            simulatorText.gameObject.SetActive(isDisplayParticleName);
        }
    }
}