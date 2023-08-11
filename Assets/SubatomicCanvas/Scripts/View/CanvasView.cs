using System.Collections.Generic;
using System.Linq;
using ParticleSim.Result;
using SubatomicCanvas.Util;
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

        public UnityEvent<(int, int), CellView> OnAddCellView = new();

        public void PutDetector((int, int) position, string key, float cellSize)
        {
            foreach (var detectorPrefab in detectorPrefabs.Where(detectorPrefab => key == detectorPrefab.DetectorKey))
            {
                var detector = _cellTable[position].PutDetector(detectorPrefab);
                detector.SetSize(cellSize);
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
        
        public void ApplySimulationResult((SimulationResult, Dictionary<string, (int, int)>) resultTuple)
        {
            var (result, pathPositionTable) = resultTuple;
            
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

        public void BuildCanvas(int canvasSize, float cellSize, IEnumerable<KeyValuePair<(int, int), string>> detectorPlacements)
        {
            Debug.LogWarning("ToDo: CanvasSizeとCellSizeの変更を行うと2回実行されてしまう");
            
            // Clear
            honeycombGridView.ClearGrid();
            foreach (var (_, cell) in _cellTable)
            {
                cell.DoDestroy();
            }
            _cellTable.Clear();
            
            // Build
            honeycombGridView.DrawGrid(canvasSize, cellSize);
            
            for (var i = 1 - canvasSize; i < canvasSize; i++)
            {
                for (var j = 1 - canvasSize + Mathf.Max(i, 0); j < canvasSize + Mathf.Min(i, 0); j++)
                {
                    var position = (j, i);

                    if (_cellTable.ContainsKey(position))
                    {
                        Debug.LogError("同じ座標にセルを生成しようとしました。");
                    }

                    var cell = Instantiate(cellPrefab, transform);
                    _cellTable[position] = cell;
                    cell.SetSize(cellSize);
                    ((RectTransform)cell.transform).anchoredPosition =
                        HoneycombCoordinate.GetPosition(position, cellSize) * 1000f;

                    OnAddCellView.Invoke(position, cell);
                }
            }

            foreach (var (position, detectorKey) in detectorPlacements)
            {
                PutDetector(position, detectorKey, cellSize);
            }
        }
    }
}