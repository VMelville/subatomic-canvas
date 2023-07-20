using System;
using SubatomicCanvas.Model;
using SubatomicCanvas.Utility;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CanvasPresenter : IStartable
    {
        // Model
        [Inject] private CanvasState _canvasState;
        [Inject] private PaintToolState _paintToolState;
        
        // View
        [Inject] private CanvasView _canvasView;

        public void Start()
        {
            _canvasState.canvasSize.Subscribe(OnChangeCanvasSize);
            _canvasState.installedDetectorPositionAndKeys.ObserveAdd().Subscribe(OnAddDetector);
            _canvasState.installedDetectorPositionAndKeys.ObserveReplace().Subscribe(OnReplaceDetector);
            _canvasState.installedDetectorPositionAndKeys.ObserveRemove().Subscribe(OnRemoveDetector);
            
            _canvasView.onAddCellView.AddListener(ListenCellEvent);

            _canvasState.canvasSize.Value = 10;
        }

        private void ListenCellEvent((int, int) position, CellView cellView)
        {
            cellView.onPointerDown.AddListener(eventData => OnPointerDown(position, eventData));
            cellView.onPointerEnter.AddListener(eventData => OnPointerEnter(position, eventData));
        }

        private void OnChangeCanvasSize(int canvasSize)
        {
            _canvasView.ClearCanvas();
            _canvasView.ReloadCanvas(canvasSize);
            
            for (var i = 1 - canvasSize; i < canvasSize; i++)
            {
                for (var j = 1 - canvasSize + Mathf.Max( i, 0); j < canvasSize + Mathf.Min( i, 0); j++)
                {
                    var position = (j, i);

                    _canvasView.AddCell(position);
                }
            }
        }

        private void OnAddDetector(DictionaryAddEvent<(int, int), string> addEvent)
        {
            _canvasView.PutDetector(addEvent.Key, addEvent.Value);
        }
        
        private void OnReplaceDetector(DictionaryReplaceEvent<(int, int), string> replaceEvent)
        {
            _canvasView.RemoveDetector(replaceEvent.Key);
            _canvasView.PutDetector(replaceEvent.Key, replaceEvent.NewValue);
        }
        
        private void OnRemoveDetector(DictionaryRemoveEvent<(int, int), string> removeEvent)
        {
            _canvasView.RemoveDetector(removeEvent.Key);
        }

        private void OnPointerDown((int, int) position, PointerEventData eventData)
        {
            if (_paintToolState.activeDetectorKey.Value == PaintToolState.ViewModeKey) return;

            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    PutDetector(position);
                    break;
                case PointerEventData.InputButton.Right:
                    RemoveDetector(position);
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnPointerEnter((int, int) position, PointerEventData eventData)
        {
            if (_paintToolState.activeDetectorKey.Value == PaintToolState.ViewModeKey) return;
            
            if (eventData.dragging)
            {
                PutDetector(position);
            }
            else if (Input.GetMouseButton(1))
            {
                RemoveDetector(position);
            }
        }

        private void PutDetector((int, int) position)
        {
            var detectorKey = _paintToolState.activeDetectorKey.Value;
            _canvasState.installedDetectorPositionAndKeys[position] = detectorKey;
            
            if (!_paintToolState.isActiveSymmetry.Value) return;
            
            var subCursorPosition = HoneycombCoordinate.MakeSubCursorPosition(position);

            foreach (var subPosition in subCursorPosition)
            {
                _canvasState.installedDetectorPositionAndKeys[subPosition] = detectorKey;
            }
        }
        
        private void RemoveDetector((int, int) position)
        {
            _canvasState.installedDetectorPositionAndKeys.Remove(position);
            
            if (!_paintToolState.isActiveSymmetry.Value) return;
            
            var subCursorPosition = HoneycombCoordinate.MakeSubCursorPosition(position);

            foreach (var subPosition in subCursorPosition)
            {
                _canvasState.installedDetectorPositionAndKeys.Remove(subPosition);
            }
        }
    }
}