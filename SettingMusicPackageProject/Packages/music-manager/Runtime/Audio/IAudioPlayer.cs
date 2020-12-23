namespace Audio
{
    public delegate void DisposingHandler(IAudioPlayer audioPlayer);
    
    public interface IAudioPlayer
    {
        event DisposingHandler Disposing;
        
        string Id { get; }
        bool IsLoop { get; set; }
        bool IsMute { get; set; }
        float FadeSeconds { get; set; }
        float Volume { get; set; }
        float Pitch { get; set; }
        float SpatialBlend { get; set; }
        float StereoPan { get; set; }
        float Spread { get; set; }
        float DopplerLevel { get; set; }
        float MinDistance { get; set; }
        float MaxDistance { get; set; }
        RolloffMode RolloffMode { get; set; }
        
        void Play();
        void Pause();
        void Stop();
        void SetPosition(IPosition position);
        void Attach(object audioAttachableObject);
        void Dispose();
    }
}