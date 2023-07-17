using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SymmetryToolButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject symmetryIcon;
        [SerializeField] private GameObject asymmetryIcon;

        public UnityEvent onclick => button.onClick;

        public void SwitchSymmetryIcon(bool isSymmetry)
        {
            symmetryIcon.SetActive(isSymmetry);
            asymmetryIcon.SetActive(!isSymmetry);
        }
    }
}