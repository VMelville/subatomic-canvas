using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class MenuManagerFacade : IInitializable, IStartable
    {
        [Inject] private MenuManager _manager;
        
        [Inject] private MenuView _menuView;
        
        public void Initialize()
        {
            _menuView.OnClickMenuButton.AddListener(_manager.ToggleOpen);
            _menuView.OnChangePage.AddListener(_manager.SetPageIndex);
        }
        
        public void Start()
        {
            _manager.SetDuration(0.3f);
        }
    }
}