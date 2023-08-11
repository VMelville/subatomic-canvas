using System;
using System.Collections.Generic;
using System.Linq;
using SubatomicCanvas.Utility;

namespace SubatomicCanvas.Model
{
    [Serializable]
    public struct CanvasDataFileInfo
    {
        public string version;
        public string saveDate;
        public string createdBy;
        public string description;

        public string title;

        public List<string> usingParticleKeys;
        public Dictionary<string, string> installedDetectorPositionAndKeys;
        public SerializableVector3 magneticFieldVector;
        public float simulationWorldDepth;
        public int canvasSize;
        public float cellSize;
        public float particleEnergyMin;
        public float particleEnergyMax;
        
        public CanvasDataFileInfo(string title, CanvasManager canvasManager)
        {
            version = UnityEngine.Application.version;
            saveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            createdBy = "";
            description = "";

            this.title = title;

            usingParticleKeys = new List<string>(canvasManager.UsingParticleKeys);
            installedDetectorPositionAndKeys = canvasManager.DetectorPlacements
                .ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => kvp.Value
                    );
            magneticFieldVector = new SerializableVector3(canvasManager.MagneticFieldVector.Value);
            simulationWorldDepth = canvasManager.SimulationWorldDepth.Value;
            canvasSize = canvasManager.CanvasSize.Value;
            cellSize = canvasManager.CellSize.Value;
            particleEnergyMin = canvasManager.ParticleEnergyMin.Value;
            particleEnergyMax = canvasManager.ParticleEnergyMax.Value;
        }
    }
}