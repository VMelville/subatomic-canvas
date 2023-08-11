using System;
using System.Collections.Generic;
using System.Linq;
using ParticleSim.Result;
using SubatomicCanvas.Model;
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
        [SerializeField] private Transform detectorParent;

        private DetectorViewBase _detector;
        
        public UnityEvent<PointerEventData> OnMiddleDrag;
        // public UnityEvent<PointerEventData> OnPointerDown;
        public UnityEvent<PointerEventData> OnPointerEnter;
        public UnityEvent<PointerEventData> OnScroll;

        public UnityEvent OnBeDrawed;
        public UnityEvent OnBeElased;
        
        private void Start()
        {
            foreach (var target in rayCastTargets)
            {
                target.OnDragAsObservable()
                    .Where(e=>e.button == PointerEventData.InputButton.Middle)
                    .Subscribe(OnMiddleDrag.Invoke);
                
                target.OnPointerDownAsObservable().Subscribe(data =>
                {
                    switch (data.button)
                    {
                        case PointerEventData.InputButton.Left:
                            OnBeDrawed.Invoke();
                            break;
                        case PointerEventData.InputButton.Right:
                            OnBeElased.Invoke();
                            break;
                        case PointerEventData.InputButton.Middle:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                });

                target.OnPointerEnterAsObservable().Subscribe(data =>
                {
                    OnPointerEnter.Invoke(data);

                    if (data.dragging)
                    {
                        OnBeDrawed.Invoke();
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        OnBeElased.Invoke();
                    }
                });
                target.OnScrollAsObservable().Subscribe(OnScroll.Invoke);
            }
        }

        public void DoDestroy()
        {
            Destroy(gameObject);
        }

        public void SetSize(float size)
        {
            // transform.localScale = Vector3.one * size;
            
            foreach (var rt in rayCastTargets.Select(target => (RectTransform)target.transform))
            {
                rt.sizeDelta = new Vector2(500f, 500f * Mathf.Sqrt(3.0f)) * size;
            }
            
            var thisRt = (RectTransform)transform;
            thisRt.sizeDelta = new Vector2(800f, 800f) * size;
        }

        public DetectorViewBase PutDetector(DetectorViewBase detectorPrefab)
        {
            return _detector = Instantiate(detectorPrefab, detectorParent);
        }

        public void RemoveDetector()
        {
            foreach (Transform child in detectorParent)
            {
                Destroy(child.gameObject);
            }
        }

        public void ClearSense()
        {
            if (_detector == null) return;
            _detector.ClearSense();
        }

        public void AddSense(TrajectoryPoint trajectoryPoint)
        {
            if (_detector == null) return;
            _detector.AddSense(trajectoryPoint);
        }

        public void ReadySense()
        {
            if (_detector == null) return;
            _detector.ReadySense();
        }

        public void SeekTime(float time)
        {
            if (_detector == null) return;
            _detector.SeekTime(time);
        }
    }
}