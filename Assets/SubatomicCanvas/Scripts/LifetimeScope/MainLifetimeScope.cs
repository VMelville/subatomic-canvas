using SubatomicCanvas.Model;
using VContainer;

namespace SubatomicCanvas.LifetimeScope
{
    public class MainLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Singleton
            builder.Register<AvailableCanvasDataFiles>(Lifetime.Singleton);
            builder.Register<AvailableDetectors>(Lifetime.Singleton);
            builder.Register<AvailableParticles>(Lifetime.Singleton);
            builder.Register<CameraState>(Lifetime.Singleton);
            builder.Register<ModeState>(Lifetime.Singleton);
            builder.Register<PaintToolState>(Lifetime.Singleton);
            builder.Register<SimulationResult>(Lifetime.Singleton);
            builder.Register<TimeState>(Lifetime.Singleton);
        }
    }
}
