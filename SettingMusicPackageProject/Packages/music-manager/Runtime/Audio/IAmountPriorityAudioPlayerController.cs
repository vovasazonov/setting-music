namespace Audio
{
    public interface IAmountPriorityAudioPlayerController
    {
        bool FreeSpaceAvailable(AudioPriorityType audioPriorityType);
        void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer);
        void RemoveAudioPlayer(IAudioPlayer audioPlayer);
    }
}