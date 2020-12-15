namespace Audio
{
    public interface IAudio3DSetting
    {
        int Spread { get; set; }
        float DopplerLevel { get; set; }
        float MinDistance { get; set; }
        float MaxDistance { get; set; }
        void SetVolumeRolloff(RolloffMode rolloffMode);
    }
}