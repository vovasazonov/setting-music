namespace Audio
{
    public delegate void DisposingHandler(IAudioPlayer audioPlayer);
    
    public interface IAudioPlayer
    {
        event DisposingHandler Disposing;
        
        string Id { get; }
        bool IsMute { get; set; }
        float FadeSeconds { get; set; }
        float Volume { get; set; }

        void Play();
        void Stop();
        void SetPosition(IPosition position);
        void Attach(object audioAttachableObject);
        void Dispose();
    }
}