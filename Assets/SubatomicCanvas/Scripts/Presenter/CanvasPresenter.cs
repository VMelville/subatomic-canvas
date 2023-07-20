﻿using SubatomicCanvas.Model;
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
        
        // View
        [Inject] private CanvasView _canvasView;

        public void Start()
        {
            _canvasState.canvasData.Subscribe(OnChangeCanvasState);
            
            _canvasView.onAddCellView.AddListener(ListenCellEvent);

            var canvasSize = _canvasState.canvasData.Value.canvasSize;
            
            for (var i = 1 - canvasSize; i < canvasSize; i++)
            {
                for (var j = 1 - canvasSize + Mathf.Max( i, 0); j < canvasSize + Mathf.Min( i, 0); j++)
                {
                    var position = (j, i);

                    _canvasView.AddCell(position);
                }
            }
        }

        private void ListenCellEvent((int, int) position, CellView cellView)
        {
            cellView.onPointerDown.AddListener(eventData => OnPointerDown(position, eventData));
            cellView.onPointerEnter.AddListener(eventData => OnPointerEnter(position, eventData));
        }

        private void OnChangeCanvasState(CanvasData data)
        {
            _canvasView.ClearCanvas();
            _canvasView.ReloadCanvas(data.canvasSize);
        }

        private void OnPointerDown((int, int) position, PointerEventData eventData)
        {
            var (x, y) = position;
        }
        
        private void  OnPointerEnter((int, int) position, PointerEventData eventData)
        {
            var (x, y) = position;
        }
    }
}