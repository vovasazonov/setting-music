namespace Audio
{
    public interface IAudioCollection : IIdentifiable<string>
    {
        event StartPlayAudioHandler StartPlay;
        event FinishPlayAudioHandler FinishPlay;
        IAudioPlayer GetAudio(string idAudio);
        void SetLimitPlaySameAudioTogether(string nameAudio, int maxAmountAudio = 2);
        void PlayAll();
        void PauseAll();
        void StopAll();
        void MuteAll(bool isMute);
        void SetVolumeAll(float volume);
    }
}