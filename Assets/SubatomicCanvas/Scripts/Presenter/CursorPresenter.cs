using SubatomicCanvas.Model;
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
        [Inject] private CanvasState _canvasState;
        
        // View
        [Inject] private CanvasView _canvasView;
        [Inject] private CursorView _cursorView;

        // CanvasViewのStartよりも早くAddListenerを設定する必要があります。そのためInitializeです。
        public void Initialize()
        {
            // ToDo: key!="ViewMode" は暫定的な対処です。変更になる可能性が高いと思われます。
            _paintToolState.ActiveDetectorKey.Select(key => key!="ViewMode").Subscribe(_cursorView.SetActiveCursor);
            _paintToolState.IsActiveSymmetry.Subscribe(_cursorView.SetActiveSubCursor);

            _canvasState.CellSize.Subscribe(_cursorView.SetCellSize);
            
            _canvasView.OnAddCellView.AddListener((position, view) => 
                view.OnPointerEnter.AddListener(_ => _cursorView.SetPosition(position)));
        }
    }
}