namespace Audio
{
    public interface IAudioCollection
    {
        string Name { get; }
        float VolumeAll { get; set; }
        bool IsMuteAll { get; set; }
        
        IAudioPlayer GetAudio(string nameSound);
        void SetLimitPlaySameAudioTogether(string nameAudio, int maxAmountAudio = 2);
        void SetBeginningAll();
        void PlayAll();
        void PauseAll();
    }
}