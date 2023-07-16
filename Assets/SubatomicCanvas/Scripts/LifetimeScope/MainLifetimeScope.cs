using SubatomicCanvas.Model;
using SubatomicCanvas.Presenter;
using SubatomicCanvas.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.LifetimeScope
{
    public class MainLifetimeScope : VContainer.Unity.LifetimeScope
    {
        [SerializeField] private CameraView cameraView;
        [SerializeField] private CanvasView canvasView;
        [SerializeField] private PaintToolView paintToolView;
        [SerializeField] private SaveLoadView saveLoadView;
        [SerializeField] private TimeView timeView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterComponent(cameraView);
            builder.RegisterComponent(canvasView);
            builder.RegisterComponent(paintToolView);
            builder.RegisterComponent(saveLoadView);
            builder.RegisterComponent(timeView);
            
            // Singleton
            builder.Register<AvailableCanvasDataFiles>(Lifetime.Singleton);
            builder.Register<AvailableDetectors>(Lifetime.Singleton);
            builder.Register<AvailableParticles>(Lifetime.Singleton);
            builder.Register<CameraState>(Lifetime.Singleton);
            builder.Register<CanvasState>(Lifetime.Singleton);
            builder.Register<ModeState>(Lifetime.Singleton);
            builder.Register<PaintToolState>(Lifetime.Singleton);
            builder.Register<SimulationResult>(Lifetime.Singleton);
            builder.Register<TimeState>(Lifetime.Singleton);
            
            // Presenter
            builder.RegisterEntryPoint<CameraPresenter>();
            builder.RegisterEntryPoint<CanvasPresenter>();
            builder.RegisterEntryPoint<DefaultSettingsPresenter>();
            builder.RegisterEntryPoint<PaintToolPresenter>();
            builder.RegisterEntryPoint<SaveLoadPresenter>();
            builder.RegisterEntryPoint<TimePresenter>();
        }
    }
}
