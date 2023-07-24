using SubatomicCanvas.Model;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class DefaultSettingsPresenter : IStartable
    {
        [Inject] private AvailableDetectors _availableDetectors;
        [Inject] private AvailableParticles _availableParticles;

        public void Start()
        {
            var trackDetector = new Detector();
            var calorimeter = new Detector();
            var absorber = new Detector();
            
            _availableDetectors.detectorDict.Add("TrackDetector", trackDetector);
            _availableDetectors.detectorDict.Add("Calorimeter", calorimeter);
            _availableDetectors.detectorDict.Add("Absorber", absorber);

            _availableParticles.particleDict["gamma"] = new Particle { pdgName = "gamma", displayName = "光子" };
            _availableParticles.particleDict["e-"] = new Particle { pdgName = "e-", displayName = "電子" };
            _availableParticles.particleDict["mu-"] = new Particle { pdgName = "mu-", displayName = "ミューオン" };
            _availableParticles.particleDict["pi0"] = new Particle { pdgName = "pi0", displayName = "π0粒子" };
            _availableParticles.particleDict["pi+"] = new Particle { pdgName = "pi+", displayName = "π+粒子" };
            _availableParticles.particleDict["kaon0S"] = new Particle { pdgName = "kaon0S", displayName = "Ks粒子" };
            _availableParticles.particleDict["Upsilon"] = new Particle { pdgName = "Upsilon", displayName = "Υ粒子" };
            _availableParticles.particleDict["lambda_b"] = new Particle { pdgName = "lambda_b", displayName = "Λb粒子" };
        }
    }
}