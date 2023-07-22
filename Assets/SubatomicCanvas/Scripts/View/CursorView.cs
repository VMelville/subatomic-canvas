using System.Collections.Generic;
using SubatomicCanvas.Utility;
using SubatomicCanvas.Utility.Tween;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public class CursorView : MonoBehaviour
    {
        [SerializeField] private GameObject cursorParent;
        [SerializeField] private RectTransform mainCursor;
        [SerializeField] private List<RectTransform> subCursor;

        public void SetPosition((int, int) position)
        {
            mainCursor.TweenAnchorPosition(HoneycombCoordinate.GetPosition(position) * 1000f, 0.2f);

            var subCursorPos = HoneycombCoordinate.MakeSubCursorPosition(position);
            
            for (var k = 0; k < 5; k++)
            {
                subCursor[k].TweenAnchorPosition(HoneycombCoordinate.GetPosition(subCursorPos[k]) * 1000f, 0.2f);
            }
        }

        public void SetActiveCursor(bool isActive)
        {
            cursorParent.SetActive(isActive);
        }

        public void SetActiveSubCursor(bool isActive)
        {
            foreach (var rt in subCursor)
            {
                rt.gameObject.SetActive(isActive);
            }
        }
    }
}