namespace Audio
{
    public interface IAudioPool
    {
        IAudioPlayer Take(string idAudio);
    }
}