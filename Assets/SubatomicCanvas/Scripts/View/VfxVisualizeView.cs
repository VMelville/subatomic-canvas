using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ParticleSim.Result;
using UnityEngine;
using UnityEngine.VFX;

namespace SubatomicCanvas.View
{
    public class VfxVisualizeView : MonoBehaviour
    {
        [SerializeField] private VisualEffect vfx;

        private GraphicsBuffer _chargeDctBuffer;
        private GraphicsBuffer _coefsDctBuffer;
        private GraphicsBuffer _meansDctBuffer;
        private GraphicsBuffer _startTimesDctBuffer;
        private GraphicsBuffer _endTimesDctBuffer;

        private GraphicsBuffer _chargeLinearBuffer;
        private GraphicsBuffer _slopeLinearBuffer;
        private GraphicsBuffer _interceptLinearBuffer;
        private GraphicsBuffer _startTimesLinearBuffer;
        private GraphicsBuffer _endTimesLinearBuffer;
        
        public void PlayVfx(List<LinerTrajectory> line)
        {
            vfx.Reinit();
            
            if (line.Count <= 0) return;

            _chargeLinearBuffer?.Release();
            _chargeLinearBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, line.Count, Marshal.SizeOf<float>());
            _chargeLinearBuffer.SetData(line.Select(x => x.Charge).ToArray());
            vfx.SetGraphicsBuffer("Charge", _chargeLinearBuffer);

            _slopeLinearBuffer?.Release();
            _slopeLinearBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, line.Count, Marshal.SizeOf<Vector3>());
            _slopeLinearBuffer.SetData(line.Select(x => x.Slope).ToArray());
            vfx.SetGraphicsBuffer("Slope", _slopeLinearBuffer);

            _interceptLinearBuffer?.Release();
            _interceptLinearBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, line.Count, Marshal.SizeOf<Vector3>());
            _interceptLinearBuffer.SetData(line.Select(x => x.Intercept).ToArray());
            vfx.SetGraphicsBuffer("Intercept", _interceptLinearBuffer);

            _startTimesLinearBuffer?.Release();
            _startTimesLinearBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, line.Count, Marshal.SizeOf<float>());
            _startTimesLinearBuffer.SetData(line.Select(x => x.StartTime).ToArray()); 
            vfx.SetGraphicsBuffer("StartTime", _startTimesLinearBuffer);

            _endTimesLinearBuffer?.Release();
            _endTimesLinearBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, line.Count, Marshal.SizeOf<float>());
            _endTimesLinearBuffer.SetData(line.Select(x => x.EndTime).ToArray());
            vfx.SetGraphicsBuffer("EndTime", _endTimesLinearBuffer);

            vfx.SetInt("Count", line.Count);
            vfx.Play();
        }

        public void OnPassesTime(float time)
        {
            vfx.SetFloat("NowTime", time);
        }

        public void OnDestroy()
        {
            _chargeDctBuffer?.Release();
            _coefsDctBuffer?.Release();
            _meansDctBuffer?.Release();
            _startTimesDctBuffer?.Release();
            _endTimesDctBuffer?.Release();

            _chargeLinearBuffer?.Release();
            _slopeLinearBuffer?.Release();
            _interceptLinearBuffer?.Release();
            _startTimesLinearBuffer?.Release();
            _endTimesLinearBuffer?.Release();
        }
    }
}