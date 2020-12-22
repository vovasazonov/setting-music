namespace Audio
{
    public interface IAmountPriorityAudioPlayerController
    {
        bool CheckSpaceAvailable(AudioPriorityType audioPriorityType);
        void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer);
        void RemoveAudioPlayer(IAudioPlayer audioPlayer);
    }
}