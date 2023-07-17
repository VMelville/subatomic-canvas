namespace SubatomicCanvas.Model
{
    public enum SnapshotStateType
    {
        NormalTime, // UIが消されていない平常時
        PrePare, // UIが消されていない かつ まだスナップショットが撮られていない
        Standby, // UIが消されている かつ まだスナップショットが撮られていない
        Took // UIが消されている かつ もうスナップショットを撮った
    }
}