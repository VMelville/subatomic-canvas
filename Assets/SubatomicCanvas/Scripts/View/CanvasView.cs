using System.Collections.Generic;
using System.Linq;
using SubatomicCanvas.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace SubatomicCanvas.View
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private HoneycombGridView honeycombGridView;
        [SerializeField] private CellView cellPrefab;
        [SerializeField] private List<DetectorViewBase> detectorPrefabs;

        private Dictionary<(int, int), CellView> _cellTable = new();

        public UnityEvent<(int, int), CellView> onAddCellView = new();

        public void ClearCanvas()
        {
            honeycombGridView.ClearGrid();
        }
        
        public void ReloadCanvas(int canvasSize)
        {
            honeycombGridView.DrawGrid(canvasSize);
        }

        public void ClearCellTable()
        {
            foreach (var (_, cell) in _cellTable)
            {
                cell.DoDestroy();
            }
            _cellTable.Clear();
        }
        
        public void AddCell((int, int) position)
        {
            if (_cellTable.ContainsKey(position))
            {
                Debug.LogError("同じ座標にセルを生成しようとしました。");
            }

            var cell = Instantiate(cellPrefab, transform);
            _cellTable[position] = cell;
            ((RectTransform)cell.transform).anchoredPosition = HoneycombCoordinate.GetPosition(position) * 1000f;
            
            onAddCellView.Invoke(position, cell);
        }

        public void PutDetector((int, int) position, string key)
        {
            foreach (var detectorPrefab in detectorPrefabs.Where(detectorPrefab => key == detectorPrefab.DetectorKey))
            {
                _cellTable[position].PutDetector(detectorPrefab);
            }
        }

        public void RemoveDetector((int, int) position)
        {
            _cellTable[position].RemoveDetector();
        }
    }
}