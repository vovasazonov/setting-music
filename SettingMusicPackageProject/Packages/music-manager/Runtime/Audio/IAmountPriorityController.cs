namespace Audio
{
    public interface IAmountPriorityController
    {
        bool CheckSpaceAvailable(AudioPriorityType audioPriorityType);
        void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer);
        void RemoveAudioPlayer(IAudioPlayer audioPlayer);
    }
}