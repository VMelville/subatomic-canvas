using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class ParticleShelfView : MonoBehaviour
    {
        [SerializeField] private Button allOnButton;
        [SerializeField] private Button allOffButton;
        [SerializeField] private Transform contentTransform;
        [SerializeField] private ParticleToggleView togglePrefab;

        public UnityEvent<string, bool> onValueChanged;

        private readonly List<ParticleToggleView> _toggles = new ();

        private void Start()
        {
            allOnButton.onClick.AddListener(AllOn);
            allOffButton.onClick.AddListener(AllOff);
        }
        
        public void AddNewToggle(string particleName, float particleEnergy)
        {
            var toggle = Instantiate(togglePrefab, contentTransform);
            toggle.onValueChanged.AddListener(isOn => onValueChanged.Invoke(particleName, isOn));
            toggle.SetMainText(particleName);
            toggle.SetSubText(particleEnergy.ToString("F2") + " MeV");
            onValueChanged.Invoke(particleName, toggle.GetIsOn());
            
            _toggles.Add(toggle);
        }

        private void AllOn()
        {
            foreach (var toggle in _toggles)
            {
                toggle.SetIsOn(true);
            }
        }
        
        private void AllOff()
        {
            foreach (var toggle in _toggles)
            {
                toggle.SetIsOn(false);
            }
        }
    }
}