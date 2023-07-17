using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class ParticleShelfView : MonoBehaviour
    {
        [SerializeField] private Button allOnButton;
        [SerializeField] private Button allOffButton;
        [SerializeField] private Transform contentTransform;

        public UnityEvent onClickAllOn => allOnButton.onClick;
        public UnityEvent onClickAllOff => allOffButton.onClick;

        public Object InstantiatePrefab(Object prefab)
        {
            return Instantiate(prefab, contentTransform);
        }
    }
}