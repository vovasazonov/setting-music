namespace Audio
{
    public delegate void PlayHandler(IAudioPlayer audioPlayer);

    public interface IAudioPlayer
    {
        event PlayHandler PlayHandler;
        int Priority { get; set; }
        bool IsLoop { get; set; }
        bool IsMute { get; set; }
        float Volume { get; set; }
        float Pitch { get; set; }
        void Play();
        void Stop();
        void Pause();
    }
}