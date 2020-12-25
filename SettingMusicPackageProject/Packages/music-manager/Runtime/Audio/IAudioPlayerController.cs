namespace Audio
{
    internal interface IAudioPlayerController
    {
        bool IsAmountPlayingLessLimit();
        IAudioPlayer GetAudioPlayer();
    }
}