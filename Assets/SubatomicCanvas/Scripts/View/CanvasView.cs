using UnityEngine;

namespace SubatomicCanvas.View
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private HoneycombGridView honeycombGridView;

        public void ClearCanvas()
        {
            honeycombGridView.ClearGrid();
        }
        
        public void ReloadCanvas(int canvasSize)
        {
            honeycombGridView.DrawGrid(canvasSize);
        }
    }
}