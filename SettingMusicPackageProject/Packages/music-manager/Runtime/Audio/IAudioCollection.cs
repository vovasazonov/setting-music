namespace Audio
{
    public interface IAudioCollection
    {
        string Id { get; }
        
        void PlayAll();
        void StopAll();
        void MuteAll(bool isMute);
        void SetVolumeAll(float volume);
    }
}