﻿using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CursorPresenter : IInitializable
    {
        // Model
        [Inject] private PaintToolState _paintToolState;
        
        // View
        [Inject] private CanvasView _canvasView;
        [Inject] private CursorView _cursorView;

        // CanvasViewのStartよりも早くAddListenerを設定する必要があります。そのためInitializeです。
        public void Initialize()
        {
            // ToDo: key!="ViewMode" は暫定的な対処です。変更になる可能性が高いと思われます。
            _paintToolState.activeDetectorKey.Select(key => key!="ViewMode").Subscribe(_cursorView.SetActiveCursor);
            _paintToolState.isActiveSymmetry.Subscribe(_cursorView.SetActiveSubCursor);
            
            _canvasView.onAddCellView.AddListener(ListenCellEvent);
        }

        private void ListenCellEvent((int, int) position, CellView cellView)
        {
            cellView.onPointerEnter.AddListener(_ => _cursorView.SetPosition(position));
        }
    }
}