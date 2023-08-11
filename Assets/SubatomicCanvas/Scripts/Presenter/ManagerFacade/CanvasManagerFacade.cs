using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CanvasManagerFacade : IInitializable, IStartable
    {
        [Inject] private CanvasManager _manager;
        
        [Inject] private PaintToolManager _paintToolManager;
        [Inject] private CameraManager _cameraManager;
        
        [Inject] private ParticleShelfView _particleShelfView;
        [Inject] private CanvasView _canvasView;
        [Inject] private SettingView _settingView;

        public void Initialize()
        {
            _settingView.OnEndEditCanvasSize.AddListener(s => _manager.SetCanvasSize(SafeParseUtil.SafeParseInt(s, 10, 1, 30)));
            _settingView.OnEndEditCellSize.AddListener(s => _manager.SetCellSize(SafeParseUtil.SafeParseFloat(s, 0.1f, 0.01f, 1f)));
            _settingView.OnEndEditSimulationWorldDepth.AddListener(s => _manager.SetSimulationWorldDepth(SafeParseUtil.SafeParseFloat(s, 10f, 0.01f, 100f)));
            _settingView.OnEndEditParticleEnergyMin.AddListener(s => _manager.TrySetParticleEnergyMin(SafeParseUtil.SafeParseFloat(s, 100f, 0f, 100000f)));
            _settingView.OnEndEditParticleEnergyMax.AddListener(s => _manager.TrySetParticleEnergyMax(SafeParseUtil.SafeParseFloat(s, 300f, 0f, 100000f)));
            _settingView.OnEndEditMagneticFieldX.AddListener(s => _manager.SetMagneticFieldX(SafeParseUtil.SafeParseFloat(s, 0f, -10000f, 10000f)));
            _settingView.OnEndEditMagneticFieldY.AddListener(s => _manager.SetMagneticFieldY(SafeParseUtil.SafeParseFloat(s, 0f, -10000f, 10000f)));
            _settingView.OnEndEditMagneticFieldZ.AddListener(s => _manager.SetMagneticFieldZ(SafeParseUtil.SafeParseFloat(s, 0f, -10000f, 10000f)));
            
            _canvasView.OnAddCellView.AddListener((position, view) =>
            {
                view.OnBeDrawed.AddListener(() =>
                {
                    if (_paintToolManager.ActiveDetectorKey.Value == PaintToolManager.ViewModeKey) return;
                    _manager.PutDetector(position, _paintToolManager.ActiveDetectorKey.Value, _paintToolManager.IsActiveSymmetry.Value);
                });
                
                view.OnBeElased.AddListener(() =>
                {
                    if (_paintToolManager.ActiveDetectorKey.Value == PaintToolManager.ViewModeKey) return;
                    _manager.RemoveDetector(position, _paintToolManager.IsActiveSymmetry.Value);
                });
            });
            
            _particleShelfView.OnValueChanged.AddListener(_manager.SetParticleState);
        }

        public void Start()
        {
            _manager.SetCanvasSize(10);
        }
    }
}