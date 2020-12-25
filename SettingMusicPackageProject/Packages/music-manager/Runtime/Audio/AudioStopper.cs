﻿namespace Audio
{
    internal readonly struct AudioStopper : IAudioStopper
    {
        private readonly IAudioPlayer _audioPlayer;

        internal AudioStopper(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Stop()
        {
            _audioPlayer?.Stop();
        }
    }
}