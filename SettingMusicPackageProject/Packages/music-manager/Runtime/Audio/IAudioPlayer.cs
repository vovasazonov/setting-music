namespace Audio
{
    public delegate void DisposingHandler(IAudioPlayer audioPlayer);
    
    public interface IAudioPlayer
    {
        event DisposingHandler Disposing;
        
        string Id { get; }
        float FadeSeconds { set; }
        bool IsMute { set; }
        float PercentageVolume { set; }

        void Play();
        void Stop();
        void SetPosition(IPosition position);
        void Attach(object audioAttachableObject);
        void Dispose();
    }
}