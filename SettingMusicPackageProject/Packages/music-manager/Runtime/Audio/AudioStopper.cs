namespace Audio
{
    public readonly struct AudioStopper : IAudioStopper
    {
        private readonly IAudioPlayer _audioPlayer;

        public AudioStopper(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Stop()
        {
            _audioPlayer?.Stop();
        }
    }
}