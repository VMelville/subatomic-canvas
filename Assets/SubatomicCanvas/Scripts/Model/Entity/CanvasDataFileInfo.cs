using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public Vector3 magneticFieldVector;
        public int canvasSize;
        public float cellSize;
        
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
            magneticFieldVector = canvasState.magneticFieldVector.Value;
            canvasSize = canvasState.canvasSize.Value;
            cellSize = canvasState.cellSize.Value;
        }
    }
}