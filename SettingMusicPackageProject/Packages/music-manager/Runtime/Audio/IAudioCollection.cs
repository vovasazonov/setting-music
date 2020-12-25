namespace Audio
{
    public interface IAudioCollection
    {
        string Id { get; }
        
        void StopAll();
        void MuteAll(bool isMute);
        void SetVolumeAll(float volume);
    }
}