namespace Audio
{
    internal interface IAmountPriorityController
    {
        bool CheckSpaceAvailable(AudioPriorityType audioPriorityType);
        void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer);
        void RemoveAudioPlayer(IAudioPlayer audioPlayer);
    }
}