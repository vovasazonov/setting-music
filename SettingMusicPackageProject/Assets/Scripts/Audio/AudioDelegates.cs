namespace Audio
{
    public delegate void CheckAllowPlayHandler(IAudioPlayer audioPlayer, out bool isAllowPlay,bool stopAudioToAllow);
    public delegate void StartPlayAudioHandler(IAudioPlayer audioPlayer);
    public delegate void FinishPlayAudioHandler(IAudioPlayer audioPlayer);
}