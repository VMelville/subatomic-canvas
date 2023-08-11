using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class MenuViewFacade : ControllerBase, IInitializable
    {
        [Inject] private MenuView _view;
        
        [Inject] private MenuManager _menuManager;

        public void Initialize()
        {
            _menuManager.IsOpen
                .Subscribe(isOpen => _view.SetOpenCloseState(isOpen, _menuManager.EasingDuration.Value))
                .AddTo(this);
            
            _menuManager.PageIndex
                .Subscribe(_view.SetPageIndex)
                .AddTo(this);
        }
    }
}