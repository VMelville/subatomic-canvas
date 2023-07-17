using SubatomicCanvas.Model;
using SubatomicCanvas.Model.UseCase;
using SubatomicCanvas.Presenter;
using SubatomicCanvas.View;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.LifetimeScope
{
    public class MainLifetimeScope : VContainer.Unity.LifetimeScope
    {
        // Memo: アルファベット順に並べてください　追加し忘れているものがある場合に気づきやすいので
        
        [SerializeField] private CameraView cameraView;
        [SerializeField] private CanvasView canvasView;
        [SerializeField] private MenuView menuView;
        [SerializeField] private PaintToolView paintToolView;
        [SerializeField] private ParticleShelfView particleShelfView;
        [FormerlySerializedAs("runButtonView")] [SerializeField] private SimulatorView simulatorView;
        [SerializeField] private SaveLoadView saveLoadView;
        [SerializeField] private SnapshotButtonView snapshotButtonView;
        [SerializeField] private TimeView timeView;
        [SerializeField] private UiVisibleView uiVisibleView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // Model - ReactiveEntity
            builder.Register<AvailableCanvasDataFiles>(Lifetime.Singleton);
            builder.Register<AvailableDetectors>(Lifetime.Singleton);
            builder.Register<AvailableParticles>(Lifetime.Singleton);
            builder.Register<CameraState>(Lifetime.Singleton);
            builder.Register<CanvasState>(Lifetime.Singleton);
            builder.Register<MenuState>(Lifetime.Singleton);
            builder.Register<ModeState>(Lifetime.Singleton);
            builder.Register<PaintToolState>(Lifetime.Singleton);
            builder.Register<SimulationResult>(Lifetime.Singleton);
            builder.Register<SnapshotState>(Lifetime.Singleton);
            builder.Register<TimeState>(Lifetime.Singleton);
            
            // Model - UseCase
            builder.Register<SimulationUseCase>(Lifetime.Singleton);
            builder.Register<SnapshotUseCase>(Lifetime.Singleton);
            
            // View
            builder.RegisterComponent(cameraView);
            builder.RegisterComponent(canvasView);
            builder.RegisterComponent(menuView);
            builder.RegisterComponent(paintToolView);
            builder.RegisterComponent(particleShelfView);
            builder.RegisterComponent(simulatorView);
            builder.RegisterComponent(saveLoadView);
            builder.RegisterComponent(snapshotButtonView);
            builder.RegisterComponent(timeView);
            builder.RegisterComponent(uiVisibleView);
            
            // Presenter
            builder.RegisterEntryPoint<CameraPresenter>();
            builder.RegisterEntryPoint<CanvasPresenter>();
            builder.RegisterEntryPoint<DefaultSettingsPresenter>();
            builder.RegisterEntryPoint<MenuPresenter>();
            builder.RegisterEntryPoint<PaintToolPresenter>();
            builder.RegisterEntryPoint<ParticleShelfPresenter>();
            builder.RegisterEntryPoint<SaveLoadPresenter>();
            builder.RegisterEntryPoint<SimulationPresenter>();
            builder.RegisterEntryPoint<SnapshotPresenter>();
            builder.RegisterEntryPoint<TimePresenter>();
        }
    }
}
