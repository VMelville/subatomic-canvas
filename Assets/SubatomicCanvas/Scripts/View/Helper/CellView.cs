using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private List<Image> rayCastTargets;

        public UnityEvent<PointerEventData> onMiddleDrag;
        public UnityEvent<PointerEventData> onPointerDown;
        public UnityEvent<PointerEventData> onPointerEnter;
        public UnityEvent<PointerEventData> onScroll;

        private void Start()
        {
            foreach (var target in rayCastTargets)
            {
                target.OnDragAsObservable()
                    .Where(e=>e.button == PointerEventData.InputButton.Middle)
                    .Subscribe(onMiddleDrag.Invoke);
                target.OnPointerDownAsObservable().Subscribe(onPointerDown.Invoke);
                target.OnPointerEnterAsObservable().Subscribe(onPointerEnter.Invoke);
                target.OnScrollAsObservable().Subscribe(onScroll.Invoke);
            }
        }

        public void DoDestroy()
        {
            Destroy(this);
        }
    }
}