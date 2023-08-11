using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CursorViewFacade : ControllerBase, IInitializable
    {
        [Inject] private CursorView _view;
        
        [Inject] private PaintToolManager _paintToolManager;
        [Inject] private CanvasManager _canvasManager;
        [Inject] private CanvasView _canvasView;
        
        public void Initialize()
        {
            // ToDo: key!="ViewMode" は暫定的な対処です。変更になる可能性が高いと思われます。
            _paintToolManager.ActiveDetectorKey.Select(key => key!="ViewMode")
                .Subscribe(_view.SetActiveCursor)
                .AddTo(this);
            
            _paintToolManager.IsActiveSymmetry
                .Subscribe(_view.SetActiveSubCursor)
                .AddTo(this);

            _canvasManager.CellSize
                .Subscribe(_view.SetCellSize)
                .AddTo(this);
            
            _canvasView.OnAddCellView.AddListener((position, view) => view.OnPointerEnter.AddListener(_ => _view.SetPosition(position)));
        }
    }
}