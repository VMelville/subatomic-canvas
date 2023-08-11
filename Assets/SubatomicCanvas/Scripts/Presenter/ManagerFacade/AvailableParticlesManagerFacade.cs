﻿using SubatomicCanvas.Model;
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
            _manager.Add("e+", new Particle { pdgName = "e+", displayName = "陽電子" });
            _manager.Add("mu-", new Particle { pdgName = "mu-", displayName = "ミューオン" });
            _manager.Add("mu+", new Particle { pdgName = "mu+", displayName = "反ミューオン" });
            _manager.Add("tau-", new Particle { pdgName = "tau-", displayName = "タウオン" });
            _manager.Add("tau+", new Particle { pdgName = "tau+", displayName = "反タウオン" });
            _manager.Add("nu_e", new Particle { pdgName = "nu_e", displayName = "電子ニュートリノ" });
            _manager.Add("anti_nu_e", new Particle { pdgName = "anti_nu_e", displayName = "反電子ニュートリノ" });
            _manager.Add("nu_mu", new Particle { pdgName = "nu_mu", displayName = "ミューニュートリノ" });
            _manager.Add("anti_nu_mu", new Particle { pdgName = "anti_nu_mu", displayName = "反ミューニュートリノ" });
            _manager.Add("nu_tau", new Particle { pdgName = "nu_tau", displayName = "タウニュートリノ" });
            _manager.Add("anti_nu_tau", new Particle { pdgName = "anti_nu_tau", displayName = "反タウニュートリノ" });
            _manager.Add("pi0", new Particle { pdgName = "pi0", displayName = "π⁰中間子" });
            _manager.Add("pi-", new Particle { pdgName = "pi-", displayName = "π⁻中間子" });
            _manager.Add("pi+", new Particle { pdgName = "pi+", displayName = "π⁺中間子" });
            _manager.Add("kaon-", new Particle { pdgName = "kaon-", displayName = "K⁻中間子" });
            _manager.Add("kaon+", new Particle { pdgName = "kaon+", displayName = "K⁺中間子" });
            _manager.Add("kaon0", new Particle { pdgName = "kaon0", displayName = "K⁰中間子" });
            _manager.Add("anti_kaon0", new Particle { pdgName = "anti_kaon0", displayName = "反K⁰中間子" });
            _manager.Add("kaon0L", new Particle { pdgName = "kaon0L", displayName = "K_L中間子" });
            _manager.Add("kaon0S", new Particle { pdgName = "kaon0S", displayName = "K_S中間子" });
            _manager.Add("eta", new Particle { pdgName = "eta", displayName = "η中間子" });
            _manager.Add("rho0", new Particle { pdgName = "rho0", displayName = "ρ⁰中間子" });
            _manager.Add("rho-", new Particle { pdgName = "rho-", displayName = "ρ⁻中間子" });
            _manager.Add("rho+", new Particle { pdgName = "rho+", displayName = "ρ⁺中間子" });
            _manager.Add("omega", new Particle { pdgName = "omega", displayName = "ω中間子" });
            _manager.Add("k_star-", new Particle { pdgName = "k_star-", displayName = "K*⁻中間子" });
            _manager.Add("k_star+", new Particle { pdgName = "k_star+", displayName = "K*⁺中間子" });
            _manager.Add("k_star0", new Particle { pdgName = "k_star0", displayName = "K*⁰中間子" });
            _manager.Add("anti_k_star0", new Particle { pdgName = "anti_k_star0", displayName = "反K*⁰中間子" });
            _manager.Add("eta_prime", new Particle { pdgName = "eta_prime", displayName = "η'中間子" });
            _manager.Add("phi", new Particle { pdgName = "phi", displayName = "φ中間子" });
            _manager.Add("D0", new Particle { pdgName = "D0", displayName = "D⁰中間子" });
            _manager.Add("anti_D0", new Particle { pdgName = "anti_D0", displayName = "反D⁰中間子" });
            _manager.Add("D-", new Particle { pdgName = "D-", displayName = "D⁻中間子" });
            _manager.Add("D+", new Particle { pdgName = "D+", displayName = "D⁺中間子" });
            _manager.Add("Ds-", new Particle { pdgName = "Ds-", displayName = "Ds⁻中間子" });
            _manager.Add("Ds+", new Particle { pdgName = "Ds+", displayName = "Ds⁺中間子" });
            _manager.Add("etac", new Particle { pdgName = "etac", displayName = "ηc中間子" });
            _manager.Add("J/psi", new Particle { pdgName = "J/psi", displayName = "J/ψ中間子" });
            _manager.Add("B-", new Particle { pdgName = "B-", displayName = "B⁻中間子" });
            _manager.Add("B+", new Particle { pdgName = "B+", displayName = "B⁺中間子" });
            _manager.Add("B0", new Particle { pdgName = "B0", displayName = "B⁰中間子" });
            _manager.Add("anti_B0", new Particle { pdgName = "anti_B0", displayName = "反B⁰中間子" });
            _manager.Add("Bs0", new Particle { pdgName = "Bs0", displayName = "Bs⁰中間子" });
            _manager.Add("anti_Bs0", new Particle { pdgName = "anti_Bs0", displayName = "反Bs⁰中間子" });
            _manager.Add("Bc-", new Particle { pdgName = "Bc-", displayName = "Bc⁻中間子" });
            _manager.Add("Bc+", new Particle { pdgName = "Bc+", displayName = "Bc⁺中間子" });
            _manager.Add("Upsilon", new Particle { pdgName = "Upsilon", displayName = "Υ中間子" });
            _manager.Add("proton", new Particle { pdgName = "proton", displayName = "陽子" });
            _manager.Add("anti_proton", new Particle { pdgName = "anti_proton", displayName = "反陽子" });
            _manager.Add("neutron", new Particle { pdgName = "neutron", displayName = "中性子" });
            _manager.Add("anti_neutron", new Particle { pdgName = "anti_neutron", displayName = "反中性子" });
            _manager.Add("lambda", new Particle { pdgName = "lambda", displayName = "Λ粒子" });
            _manager.Add("anti_lambda", new Particle { pdgName = "anti_lambda", displayName = "反Λ粒子" });
            _manager.Add("sigma+", new Particle { pdgName = "sigma+", displayName = "Σ⁺粒子" });
            _manager.Add("anti_sigma+", new Particle { pdgName = "anti_sigma+", displayName = "反Σ⁺粒子" });
            _manager.Add("sigma0", new Particle { pdgName = "sigma0", displayName = "Σ⁰粒子" });
            _manager.Add("anti_sigma0", new Particle { pdgName = "anti_sigma0", displayName = "反Σ⁰粒子" });
            _manager.Add("sigma-", new Particle { pdgName = "sigma-", displayName = "Σ⁻粒子" });
            _manager.Add("anti_sigma-", new Particle { pdgName = "anti_sigma-", displayName = "反Σ⁻粒子" });
            _manager.Add("delta-", new Particle { pdgName = "delta-", displayName = "Δ⁻粒子" });
            _manager.Add("anti_delta-", new Particle { pdgName = "anti_delta-", displayName = "反Δ⁻粒子" });
            _manager.Add("delta+", new Particle { pdgName = "delta+", displayName = "Δ⁺粒子" });
            _manager.Add("anti_delta+", new Particle { pdgName = "anti_delta+", displayName = "反Δ⁺粒子" });
            _manager.Add("delta++", new Particle { pdgName = "delta++", displayName = "Δ⁺⁺粒子" });
            _manager.Add("anti_delta++", new Particle { pdgName = "anti_delta++", displayName = "反Δ⁺⁺粒子" });
            _manager.Add("delta0", new Particle { pdgName = "delta0", displayName = "Δ⁰粒子" });
            _manager.Add("anti_delta0", new Particle { pdgName = "anti_delta0", displayName = "反Δ⁰粒子" });
            _manager.Add("xi0", new Particle { pdgName = "xi0", displayName = "Ξ⁰粒子" });
            _manager.Add("anti_xi0", new Particle { pdgName = "anti_xi0", displayName = "反Ξ⁰粒子" });
            _manager.Add("xi-", new Particle { pdgName = "xi-", displayName = "Ξ⁻粒子" });
            _manager.Add("anti_xi-", new Particle { pdgName = "anti_xi-", displayName = "反Ξ⁻粒子" });
            _manager.Add("omega-", new Particle { pdgName = "omega-", displayName = "Ω⁻粒子" });
            _manager.Add("anti_omega-", new Particle { pdgName = "anti_omega-", displayName = "反Ω⁻粒子" });
            _manager.Add("lambda_c+", new Particle { pdgName = "lambda_c+", displayName = "Λc⁺粒子" });
            _manager.Add("anti_lambda_c+", new Particle { pdgName = "anti_lambda_c+", displayName = "反Λc⁺粒子" });
            _manager.Add("sigma_c+", new Particle { pdgName = "sigma_c+", displayName = "Σc⁺粒子" });
            _manager.Add("anti_sigma_c+", new Particle { pdgName = "anti_sigma_c+", displayName = "反Σc⁺粒子" });
            _manager.Add("sigma_c0", new Particle { pdgName = "sigma_c0", displayName = "Σc⁰粒子" });
            _manager.Add("anti_sigma_c0", new Particle { pdgName = "anti_sigma_c0", displayName = "反Σc⁰粒子" });
            _manager.Add("sigma_c++", new Particle { pdgName = "sigma_c++", displayName = "Σc⁺⁺粒子" });
            _manager.Add("anti_sigma_c++", new Particle { pdgName = "anti_sigma_c++", displayName = "反Σc⁺⁺粒子" });
            _manager.Add("xi_c+", new Particle { pdgName = "xi_c+", displayName = "Ξc⁺粒子" });
            _manager.Add("anti_xi_c+", new Particle { pdgName = "anti_xi_c+", displayName = "反Ξc⁺粒子" });
            _manager.Add("xi_c0", new Particle { pdgName = "xi_c0", displayName = "Ξc⁰粒子" });
            _manager.Add("anti_xi_c0", new Particle { pdgName = "anti_xi_c0", displayName = "反Ξc⁰粒子" });
            _manager.Add("omega_c0", new Particle { pdgName = "omega_c0", displayName = "Ωc⁰粒子" });
            _manager.Add("anti_omega_c0", new Particle { pdgName = "anti_omega_c0", displayName = "反Ωc⁰粒子" });
            _manager.Add("lambda_b", new Particle { pdgName = "lambda_b", displayName = "Λb粒子" });
            _manager.Add("anti_lambda_b", new Particle { pdgName = "anti_lambda_b", displayName = "反Λb粒子" });
            _manager.Add("xi_b0", new Particle { pdgName = "xi_b0", displayName = "Ξb⁰粒子" });
            _manager.Add("anti_xi_b0", new Particle { pdgName = "anti_xi_b0", displayName = "反Ξb⁰粒子" });
            _manager.Add("xi_b-", new Particle { pdgName = "xi_b-", displayName = "Ξb⁻粒子" });
            _manager.Add("anti_xi_b-", new Particle { pdgName = "anti_xi_b-", displayName = "反Ξb⁻粒子" });
            _manager.Add("sigma_b0", new Particle { pdgName = "sigma_b0", displayName = "Σb⁰粒子" });
            _manager.Add("anti_sigma_b0", new Particle { pdgName = "anti_sigma_b0", displayName = "反Σb⁰粒子" });
            _manager.Add("sigma_b+", new Particle { pdgName = "sigma_b+", displayName = "Σb⁺粒子" });
            _manager.Add("anti_sigma_b+", new Particle { pdgName = "anti_sigma_b+", displayName = "反Σb⁺粒子" });
            _manager.Add("sigma_b-", new Particle { pdgName = "sigma_b-", displayName = "Σb⁻粒子" });
            _manager.Add("anti_sigma_b-", new Particle { pdgName = "anti_sigma_b-", displayName = "反Σb⁻粒子" });
            _manager.Add("omega_b-", new Particle { pdgName = "omega_b-", displayName = "Ωb⁻粒子" });
            _manager.Add("anti_omega_b-", new Particle { pdgName = "anti_omega_b-", displayName = "反Ωb⁻粒子" });
            _manager.Add("deuteron", new Particle { pdgName = "deuteron", displayName = "重水素原子核" });
            _manager.Add("He3", new Particle { pdgName = "He3", displayName = "ヘリウム3原子核" });
            _manager.Add("alpha", new Particle { pdgName = "alpha", displayName = "アルファ粒子" });
        }
    }
}