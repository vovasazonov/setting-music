namespace Audio
{
    public delegate void CheckAllowPlayHandler(IAudioPlayer audioPlayer, ref bool isAllowPlay);
    public delegate void StartPlayAudioHandler(IAudioPlayer audioPlayer);
    public delegate void FinishPlayAudioHandler(IAudioPlayer audioPlayer);
}