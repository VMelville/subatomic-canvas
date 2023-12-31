﻿using SubatomicCanvas.Util;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public class GridUnitView : MonoBehaviour
    {
        [SerializeField] private RectTransform lineA;
        [SerializeField] private RectTransform lineB;
        [SerializeField] private RectTransform lineC;
        
        public void SetActive((bool, bool, bool) isActive)
        {
            var (a, b, c) = isActive;
            lineA.gameObject.SetActive(a);
            lineB.gameObject.SetActive(b);
            lineC.gameObject.SetActive(c);
        }

        public void SetSize(float size)
        {
            var sizeDelta = lineA.sizeDelta;
            sizeDelta.x = size * 500f;

            lineA.sizeDelta = sizeDelta;
            lineB.sizeDelta = sizeDelta;
            lineC.sizeDelta = sizeDelta;
        }

        public void SetPosition((int, int) position, float cellSize)
        {
            var rt = (RectTransform) transform;
            var (x, y) = position;
            rt.anchoredPosition = HoneycombCoordinate.GetPosition(x + 2.0f / 3.0f, y + 1.0f / 3.0f, cellSize) * 1000f;
        }
    }
}