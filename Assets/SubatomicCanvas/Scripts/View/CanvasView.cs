using System.Collections.Generic;
using System.Linq;
using ParticleSim.Result;
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

        private readonly Dictionary<(int, int), CellView> _cellTable = new();

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
        
        public void RemoveDetectorAll()
        {
            foreach (var view in _cellTable.Values)
            {
                view.RemoveDetector();
            }
        }
        
        public void ApplySimulationResult(SimulationResult result, Dictionary<string, (int, int)> pathPositionTable)
        {
            if (result == null) return;
            
            foreach (var cell in _cellTable.Values)
            {
                cell.ClearSense();
            }

            foreach (var trajectory in result.Trajectories)
            {
                for (var i = 1; i < trajectory.Points.Count; i++)
                {
                    var point = trajectory.Points[i];

                    if (!pathPositionTable.ContainsKey(point.PreStepVolumePath)) continue;

                    var position = pathPositionTable[point.PreStepVolumePath];

                    var cell = _cellTable[position];

                    cell.AddSense(point);
                }
            }
        
            foreach (var cell in _cellTable.Values)
            {
                cell.ReadySense();
            }
        }

        public void SeekTime(float time)
        {
            foreach (var cell in _cellTable.Values)
            {
                cell.SeekTime(time);
            }
        }
    }
}