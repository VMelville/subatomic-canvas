using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class GlobalSettingManagerFacade : IStartable
    {
        [Inject] private GlobalSettingManager _manager;
        
        [Inject] private SettingView _settingView;

        public void Start()
        {
            _settingView.OnToggleDisplayParticleName.AddListener(isOn => _manager.SetIsDisplayParticleName(isOn));
            _settingView.OnToggleDisplayLineVisualizer.AddListener(isOn => _manager.SetIsDisplayLineVisualizer(isOn));
        }
    }
}