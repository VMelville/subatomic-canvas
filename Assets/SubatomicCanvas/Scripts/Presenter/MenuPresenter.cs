using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class MenuPresenter : IStartable
    {
        // Model
        [Inject] private MenuManager _menuManager;
        
        // State
        [Inject] private MenuView _menuView;

        public void Start()
        {
            _menuManager.IsOpen.Subscribe(isOpen => _menuView.SetOpenCloseState(isOpen, _menuManager.EasingDuration.Value));
            _menuManager.PageIndex.Subscribe(_menuView.SetPageIndex);

            _menuView.OnClickMenuButton.AddListener(_menuManager.ToggleOpen);
            _menuView.OnChangePage.AddListener(_menuManager.SetPageIndex);

            _menuManager.SetDuration(0.3f);
        }
    }
}