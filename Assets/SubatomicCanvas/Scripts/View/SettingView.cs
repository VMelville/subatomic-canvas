using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SettingView : MonoBehaviour
    {
        [SerializeField] private InputField canvasSizeInputField;
        [SerializeField] private InputField cellSizeInputField;
        [SerializeField] private InputField simulationWorldDepthInputField;
        [SerializeField] private InputField particleEnergyMinInputField;
        [SerializeField] private InputField particleEnergyMaxInputField;
        [SerializeField] private InputField magneticFieldXInputField;
        [SerializeField] private InputField magneticFieldYInputField;
        [SerializeField] private InputField magneticFieldZInputField;
        [SerializeField] private Toggle displayParticleNameToggle;
        [SerializeField] private Toggle displayLineVisualizerToggle;

        public InputField.EndEditEvent OnEndEditCanvasSize => canvasSizeInputField.onEndEdit;
        public InputField.EndEditEvent OnEndEditCellSize => cellSizeInputField.onEndEdit;
        public InputField.EndEditEvent OnEndEditSimulationWorldDepth => simulationWorldDepthInputField.onEndEdit;
        public InputField.EndEditEvent OnEndEditParticleEnergyMin => particleEnergyMinInputField.onEndEdit;
        public InputField.EndEditEvent OnEndEditParticleEnergyMax => particleEnergyMaxInputField.onEndEdit;
        public InputField.EndEditEvent OnEndEditMagneticFieldX => magneticFieldXInputField.onEndEdit;
        public InputField.EndEditEvent OnEndEditMagneticFieldY => magneticFieldYInputField.onEndEdit;
        public InputField.EndEditEvent OnEndEditMagneticFieldZ => magneticFieldZInputField.onEndEdit;
        public Toggle.ToggleEvent OnToggleDisplayParticleName => displayParticleNameToggle.onValueChanged;
        public Toggle.ToggleEvent OnToggleDisplayLineVisualizer => displayLineVisualizerToggle.onValueChanged;

        public void SetCanvasSize(int canvasSize) => canvasSizeInputField.text = canvasSize.ToString();
        public void SetCellSize(float cellSize) => cellSizeInputField.text = cellSize.ToString("F2");
        public void SetSimulationWorldDepth(float simulationWorldDepth) => simulationWorldDepthInputField.text = simulationWorldDepth.ToString("F2");
        public void SetParticleEnergyMin(float particleEnergyMin) => particleEnergyMinInputField.text = particleEnergyMin.ToString("F2");
        public void SetParticleEnergyMax(float particleEnergyMax) => particleEnergyMaxInputField.text = particleEnergyMax.ToString("F2");

        public void SetMagneticField(Vector3 magneticField)
        {
            magneticFieldXInputField.text = magneticField.x.ToString("F2");
            magneticFieldYInputField.text = magneticField.y.ToString("F2");
            magneticFieldZInputField.text = magneticField.z.ToString("F2");
        }

        public void SetIsDisplayParticleName(bool isDisplayParticleName) => displayParticleNameToggle.isOn = isDisplayParticleName;
        public void SetIsDisplayLineVisualizer(bool isDisplayLineVisualizer) => displayLineVisualizerToggle.isOn = isDisplayLineVisualizer;
    }
}