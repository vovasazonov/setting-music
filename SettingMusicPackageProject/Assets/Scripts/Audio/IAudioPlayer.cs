namespace Audio
{
    public interface IAudioPlayer
    {
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