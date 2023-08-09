using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SubatomicCanvas.Utility;
using UniRx;
using Vector3 = UnityEngine.Vector3;

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
        
        public CanvasDataFileInfo(string title, CanvasState canvasState)
        {
            version = UnityEngine.Application.version;
            saveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            createdBy = "";
            description = "";

            this.title = title;

            usingParticleKeys = new List<string>(canvasState.usingParticleKeys);
            installedDetectorPositionAndKeys = canvasState.installedDetectorPositionAndKeys
                .ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => kvp.Value
                    );
            magneticFieldVector = new SerializableVector3(canvasState.magneticFieldVector.Value);
            simulationWorldDepth = canvasState.simulationWorldDepth.Value;
            canvasSize = canvasState.canvasSize.Value;
            cellSize = canvasState.cellSize.Value;
            particleEnergyMin = canvasState.particleEnergyMin.Value;
            particleEnergyMax = canvasState.particleEnergyMax.Value;
        }
    }
}