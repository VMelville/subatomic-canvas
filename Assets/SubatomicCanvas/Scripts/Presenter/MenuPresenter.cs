using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class MenuPresenter : IStartable
    {
        [Inject] private MenuState _menuState;
        [Inject] private MenuView _menuView;

        public void Start()
        {
            _menuView.SetOpenCloseStateImmediately(_menuState.isOpen.Value);
            
            _menuState.isOpen.Subscribe(_menuView.SetOpenCloseState);
            _menuState.pageIndex.Subscribe(_menuView.SetPageIndex);

            _menuView.onClickMenuButton.AddListener(OnClickMenuButton);
            _menuView.onChangePage.AddListener(OnChangePage);
        }

        private void OnClickMenuButton()
        {
            _menuState.isOpen.Value ^= true;
        }

        private void OnChangePage(int pageIndex)
        {
            _menuState.pageIndex.Value = pageIndex;
        }
    }
}