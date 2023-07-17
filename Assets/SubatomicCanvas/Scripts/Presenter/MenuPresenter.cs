using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UnityEngine;
using UnityEngine.Android;
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
            Debug.LogWarning("ToDo: Menuの疎結合化");
            // _menuState.isOpen.Subscribe(_menuView)
        }
    }
}