using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SimulatorView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text simulatorText;

        public UnityEvent onClick => button.onClick;

        public void SetText(string text)
        {
            simulatorText.text = text;
        }
    }
}