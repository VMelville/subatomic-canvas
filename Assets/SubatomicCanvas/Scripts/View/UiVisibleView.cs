using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SubatomicCanvas.View
{
    public class UiVisibleView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> hideUiList;

        public UnityEvent<bool> onSetActive = new();

        public void SetIsVisible(bool isVisible)
        {
            foreach (var ui in hideUiList)
            {
                ui.SetActive(isVisible);
            }
            
            onSetActive.Invoke(isVisible);
        }
    }
}
