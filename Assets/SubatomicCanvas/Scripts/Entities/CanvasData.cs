using System.Collections.Generic;
using UnityEngine;

namespace SubatomicCanvas.Entities
{
    public class CanvasData
    {
        public HashSet<string> usingParticleKeys;
        public List<((int, int), string)> installedDetectorPositionAndKeys;
        public Vector3 magneticFieldVector;
        public float cellSize;
        public int canvasSize;
    }
}