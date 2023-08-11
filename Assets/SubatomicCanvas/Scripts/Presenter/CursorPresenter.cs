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
        [Inject] private PaintToolManager _paintToolManager;
        [Inject] private CanvasManager _canvasManager;
        
        // View
        [Inject] private CanvasView _canvasView;
        [Inject] private CursorView _cursorView;

        // CanvasViewのStartよりも早くAddListenerを設定する必要があります。そのためInitializeです。
        public void Initialize()
        {
            // ToDo: key!="ViewMode" は暫定的な対処です。変更になる可能性が高いと思われます。
            _paintToolManager.ActiveDetectorKey.Select(key => key!="ViewMode").Subscribe(_cursorView.SetActiveCursor);
            _paintToolManager.IsActiveSymmetry.Subscribe(_cursorView.SetActiveSubCursor);

            _canvasManager.CellSize.Subscribe(_cursorView.SetCellSize);
            
            _canvasView.OnAddCellView.AddListener((position, view) => 
                view.OnPointerEnter.AddListener(_ => _cursorView.SetPosition(position)));
        }
    }
}