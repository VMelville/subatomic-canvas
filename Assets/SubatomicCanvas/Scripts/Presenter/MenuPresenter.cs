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
        [Inject] private MenuState _menuState;
        
        // State
        [Inject] private MenuView _menuView;

        public void Start()
        {
            _menuState.IsOpen.Subscribe(isOpen => _menuView.SetOpenCloseState(isOpen, _menuState.EasingDuration.Value));
            _menuState.PageIndex.Subscribe(_menuView.SetPageIndex);

            _menuView.OnClickMenuButton.AddListener(_menuState.ToggleOpen);
            _menuView.OnChangePage.AddListener(_menuState.SetPageIndex);

            _menuState.SetDuration(0.3f);
        }
    }
}