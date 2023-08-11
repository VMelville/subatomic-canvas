using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class MenuViewFacade : IInitializable
    {
        [Inject] private MenuView _view;
        
        [Inject] private MenuManager _menuManager;

        public void Initialize()
        {
            _menuManager.IsOpen.Subscribe(isOpen => _view.SetOpenCloseState(isOpen, _menuManager.EasingDuration.Value));
            _menuManager.PageIndex.Subscribe(_view.SetPageIndex);
        }
    }
}