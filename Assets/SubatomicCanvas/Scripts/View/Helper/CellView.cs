﻿using System.Collections.Generic;
using ParticleSim.Result;
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

        public void PutDetector(DetectorViewBase detectorPrefab)
        {
            _detector = Instantiate(detectorPrefab, detectorParent);
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