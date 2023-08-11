using SubatomicCanvas.Model;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class AvailableParticlesManagerFacade : IStartable
    {
        [Inject] private AvailableParticlesManager _manager;

        public void Start()
        {
            _manager.Add("gamma", new Particle { pdgName = "gamma", displayName = "光子" });
            _manager.Add("e-", new Particle { pdgName = "e-", displayName = "電子" });
            _manager.Add("mu-", new Particle { pdgName = "mu-", displayName = "ミューオン" });
            _manager.Add("pi0", new Particle { pdgName = "pi0", displayName = "π0粒子" });
            _manager.Add("pi+", new Particle { pdgName = "pi+", displayName = "π+粒子" });
            _manager.Add("kaon0S", new Particle { pdgName = "kaon0S", displayName = "Ks粒子" });
            _manager.Add("Upsilon", new Particle { pdgName = "Upsilon", displayName = "Υ粒子" });
            _manager.Add("lambda_b", new Particle { pdgName = "lambda_b", displayName = "Λb粒子" });
        }
    }
}