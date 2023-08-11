using System.IO;
using System.Runtime.InteropServices;
using SubatomicCanvas.Model;
using SubatomicCanvas.Presenter;
using SubatomicCanvas.Utility;
using SubatomicCanvas.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.LifetimeScope
{
    public class MainLifetimeScope : VContainer.Unity.LifetimeScope
    {
        // Memo: アルファベット順に並べてください　追加し忘れているものがある場合に気づきやすいので
        
        [SerializeField] private CameraView cameraView;
        [SerializeField] private CanvasView canvasView;
        [SerializeField] private CursorView cursorView;
        [SerializeField] private LineVisualizeView lineVisualizeView;
        [SerializeField] private MenuView menuView;
        [SerializeField] private PaintToolView paintToolView;
        [SerializeField] private ParticleShelfView particleShelfView;
        [SerializeField] private RaycastTargetView raycastTargetView;
        [SerializeField] private SimulatorView simulatorView;
        [SerializeField] private SaveLoadView saveLoadView;
        [SerializeField] private ScreenView screenView;
        [SerializeField] private SettingView settingView;
        [SerializeField] private SnapshotButtonView snapshotButtonView;
        [SerializeField] private TimeView timeView;
        [SerializeField] private UiVisibleView uiVisibleView;
        [SerializeField] private VfxVisualizeView vfxVisualizeView;
        
        // ToDo: どのクラスよりも真っ先に実行されてほしいのでLifetimeScopeに書いちゃってるけど、もっと適切な書き方ありそう。
        [DllImport("ucrtbase", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        static extern int _wputenv_s(string var, string value);
        
        protected override void Configure(IContainerBuilder builder)
        {
            var datasetPath = Path.Combine(GetParentPath(Application.dataPath), "Geant4_Dataset");
            _wputenv_s("GEANT4_DATA_DIR", datasetPath);

            ParticleSim.Simulator.Init();
            
            // Model - ReactiveEntity
            builder.Register<SaveLoadManager>(Lifetime.Singleton);
            builder.Register<AvailableDetectors>(Lifetime.Singleton);
            builder.Register<AvailableParticles>(Lifetime.Singleton);
            builder.Register<CameraManager>(Lifetime.Singleton);
            builder.Register<CanvasManager>(Lifetime.Singleton);
            builder.Register<GlobalSettingManager>(Lifetime.Singleton);
            builder.Register<LastSimulationConditionManager>(Lifetime.Singleton);
            builder.Register<MenuManager>(Lifetime.Singleton);
            builder.Register<PaintToolManager>(Lifetime.Singleton);
            builder.Register<SnapshotManager>(Lifetime.Singleton);
            builder.Register<TimeManager>(Lifetime.Singleton);
            
            builder.Register<SampleDetectors>(Lifetime.Singleton);
            builder.Register<SimulationService>(Lifetime.Singleton);
            builder.Register<SnapshotService>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<TimeTicker>();

            // View
            builder.RegisterComponent(cameraView);
            builder.RegisterComponent(canvasView);
            builder.RegisterComponent(cursorView);
            builder.RegisterComponent(lineVisualizeView);
            builder.RegisterComponent(menuView);
            builder.RegisterComponent(paintToolView);
            builder.RegisterComponent(particleShelfView);
            builder.RegisterComponent(raycastTargetView);
            builder.RegisterComponent(simulatorView);
            builder.RegisterComponent(saveLoadView);
            builder.RegisterComponent(settingView);
            builder.RegisterComponent(screenView);
            builder.RegisterComponent(snapshotButtonView);
            builder.RegisterComponent(timeView);
            builder.RegisterComponent(uiVisibleView);
            builder.RegisterComponent(vfxVisualizeView);
            
            // Presenter
            builder.RegisterEntryPoint<CameraPresenter>();
            builder.RegisterEntryPoint<CanvasPresenter>();
            builder.RegisterEntryPoint<CursorPresenter>();
            builder.RegisterEntryPoint<DefaultSettingsPresenter>();
            builder.RegisterEntryPoint<LineVisualizePresenter>();
            builder.RegisterEntryPoint<MenuPresenter>();
            builder.RegisterEntryPoint<PaintToolPresenter>();
            builder.RegisterEntryPoint<ParticleShelfPresenter>();
            builder.RegisterEntryPoint<SaveLoadPresenter>();
            builder.RegisterEntryPoint<SettingPresenter>();
            builder.RegisterEntryPoint<SimulationPresenter>();
            builder.RegisterEntryPoint<SnapshotPresenter>();
            builder.RegisterEntryPoint<TimePresenter>();
            builder.RegisterEntryPoint<VfxVisualizePresenter>();
        }
        
        private static string GetParentPath(string path)
        {
            return new DirectoryInfo(path).Parent?.FullName;
        }
    }
}
