using UnityEngine;

namespace Audio
{
    internal class AudioSourceVolume : IVolume
    {
        private readonly AudioSource _audioSource;

        public AudioSourceVolume(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }
    }
}