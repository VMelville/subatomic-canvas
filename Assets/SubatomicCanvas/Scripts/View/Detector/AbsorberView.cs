using ParticleSim.Result;

namespace SubatomicCanvas.View
{
    public class AbsorberView : DetectorViewBase
    {
        public override string DetectorKey => "AbsorberV1";
        
        public override void ClearSense()
        {
            // Absorberはただの置物なので、シミュレーション結果から反映するものはありません。
        }
        
        public override void AddSense(TrajectoryPoint trajectoryPoint)
        {
            // Absorberはただの置物なので、シミュレーション結果から反映するものはありません。
        }

        public override void ReadySense()
        {
            // Absorberはただの置物なので、シミュレーション結果から反映するものはありません。
        }

        public override void SeekTime(float time)
        {
            // Absorberはただの置物なので、時間変化から反映するものはありません。
        }
    }
}