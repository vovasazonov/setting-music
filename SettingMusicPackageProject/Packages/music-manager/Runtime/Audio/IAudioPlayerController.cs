namespace Audio
{
    public interface IAudioPlayerController
    {
        bool IsAmountPlayingLessLimit();
        IAudioPlayer GetAudioPlayer();
    }
}