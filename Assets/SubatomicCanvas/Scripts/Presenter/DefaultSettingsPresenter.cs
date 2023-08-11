using SubatomicCanvas.Model;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class DefaultSettingsPresenter : IStartable
    {
        // Model
        [Inject] private AvailableDetectors _availableDetectors;
        [Inject] private AvailableParticles _availableParticles;

        public void Start()
        {
            var trackDetector = new Detector();
            var calorimeter = new Detector();
            var absorber = new Detector();
            
            _availableDetectors.Add("TrackDetector", trackDetector);
            _availableDetectors.Add("Calorimeter", calorimeter);
            _availableDetectors.Add("Absorber", absorber);

            _availableParticles.Add("gamma", new Particle { pdgName = "gamma", displayName = "光子" });
            _availableParticles.Add("e-", new Particle { pdgName = "e-", displayName = "電子" });
            _availableParticles.Add("mu-", new Particle { pdgName = "mu-", displayName = "ミューオン" });
            _availableParticles.Add("pi0", new Particle { pdgName = "pi0", displayName = "π0粒子" });
            _availableParticles.Add("pi+", new Particle { pdgName = "pi+", displayName = "π+粒子" });
            _availableParticles.Add("kaon0S", new Particle { pdgName = "kaon0S", displayName = "Ks粒子" });
            _availableParticles.Add("Upsilon", new Particle { pdgName = "Upsilon", displayName = "Υ粒子" });
            _availableParticles.Add("lambda_b", new Particle { pdgName = "lambda_b", displayName = "Λb粒子" });
        }
    }
}