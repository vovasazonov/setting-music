namespace Audio
{
    public interface IAudioManager
    {
        bool IsMuteSound { get; set; }
        bool IsMuteMusic { get; set; }
        float SoundVolume { get; set; }
        float MusicVolume { get; set; }

        void AddSound(IAudioPlayer soundPlayer);
        void RemoveSound(IAudioPlayer soundPlayer);
        void AddMusic(IAudioPlayer musicPlayer);
        void RemoveMusic(IAudioPlayer musicPlayer);
    }
}
