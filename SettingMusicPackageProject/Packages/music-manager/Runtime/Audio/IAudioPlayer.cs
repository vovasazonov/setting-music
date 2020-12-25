namespace Audio
{
    internal delegate void DisposingHandler(IAudioPlayer audioPlayer);
    
    internal interface IAudioPlayer
    {
        event DisposingHandler Disposing;
        
        string Id { get; }
        float FadeSeconds { set; }
        bool IsMute { set; }
        float Volume { set; }

        void Play();
        void Stop();
        void SetPosition(IPosition position);
        void Attach(object audioAttachableObject);
        void Dispose();
    }
}