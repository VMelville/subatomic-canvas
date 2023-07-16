using System.Collections.Generic;
using System.Numerics;

namespace SubatomicCanvas.Model
{
    public struct CanvasData
    {
        public string usingParticleKeys;
        public List<((int, int), string)> installedDetectorPositionAndKeys;
        public Vector3 magneticFieldVector;
        public float cellSize;
        public int canvasSize;
    }
}