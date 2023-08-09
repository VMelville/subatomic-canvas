using ParticleSim.Result;
using UnityEngine;

namespace SubatomicCanvas.View
{
    public abstract class DetectorViewBase : MonoBehaviour
    { 
        public abstract string DetectorKey { get; }
        public abstract void ClearSense();
        public abstract void SetSize(float size);
        public abstract void AddSense(TrajectoryPoint trajectoryPoint);
        public abstract void ReadySense();
        public abstract void SeekTime(float time);
    }
}