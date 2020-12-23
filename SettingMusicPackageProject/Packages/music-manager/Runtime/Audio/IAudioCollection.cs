namespace Audio
{
    public interface IAudioCollection
    {
        string Id { get; }
        
        void SetLimitPlaySameAudioTogether(string idAudio, int maxAmount);
        void PlayAll();
        void StopAll();
        void MuteAll(bool isMute);
        void SetVolumeAll(float volume);
    }
}