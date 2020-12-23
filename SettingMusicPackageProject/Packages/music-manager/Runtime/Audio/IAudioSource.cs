namespace Audio
{
    public interface IAudioSource
    {
        bool IsLoop { get; set; }
        bool IsMute { get; set; }
        float Volume { get; set; }
        float Pitch { get; set; }
        float SpatialBlend { get; set; }
        float StereoPan { get; set; }
        float Spread { get; set; }
        float DopplerLevel { get; set; }
        float MinDistance { get; set; }
        float MaxDistance { get; set; }
        RolloffMode RolloffMode { get; set; } 
        bool IsPlaying { get; }
        float Time { get; }
        
        void Play();
        void Stop();
        void SetPosition(IPosition position);
        void Attach(object audioAttachableObject);
        void SetEnable(bool isEnable);
        void SetClip(string idClip);
    }
}