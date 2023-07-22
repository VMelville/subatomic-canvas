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
            _menuState.isOpen.Subscribe(SetOpenClose);
            _menuState.pageIndex.Subscribe(_menuView.SetPageIndex);

            _menuView.onClickMenuButton.AddListener(OnClickMenuButton);
            _menuView.onChangePage.AddListener(OnChangePage);

            _menuState.easingDuration.Value = 0.3f;
        }

        private void SetOpenClose(bool isOpen) => _menuView.SetOpenCloseState(isOpen, _menuState.easingDuration.Value);

        private void OnClickMenuButton() => _menuState.isOpen.Value ^= true;

        private void OnChangePage(int pageIndex) => _menuState.pageIndex.Value = pageIndex;
    }
}