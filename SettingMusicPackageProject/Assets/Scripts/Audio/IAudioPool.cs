namespace Audio
{
    public interface IAudioPool
    {
        IAudioPlayer Take(string idAudio);
        void Return(IAudioPlayer audioPlayer);
    }
}