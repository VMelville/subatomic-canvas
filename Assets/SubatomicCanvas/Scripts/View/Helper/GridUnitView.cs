using SubatomicCanvas.Utility;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public class GridUnitView : MonoBehaviour
    {
        [SerializeField] private GameObject lineA;
        [SerializeField] private GameObject lineB;
        [SerializeField] private GameObject lineC;
        
        // ToDo: Setといいつつ、別に保持はしてない…　命名を見直すべきか、保持すべきか
        // public void SetViewModel(GridUnitViewModel viewModel)
        // {
        //     SetActive(viewModel.IsActive);
        //
        //     var rt = (RectTransform) transform;
        //     var (x, y) = viewModel.Position;
        //     rt.anchoredPosition = HoneycombCoordinate.GetPosition(x + 2.0f / 3.0f, y + 1.0f / 3.0f) * 1000f;
        // }

        public void SetActive((bool, bool, bool) isActive)
        {
            var (a, b, c) = isActive;
            lineA.SetActive(a);
            lineB.SetActive(b);
            lineC.SetActive(c);
        }

        public void SetPosition((int, int) position)
        {
            var rt = (RectTransform) transform;
            var (x, y) = position;
            rt.anchoredPosition = HoneycombCoordinate.GetPosition(x + 2.0f / 3.0f, y + 1.0f / 3.0f) * 1000f;
        }
    }
}