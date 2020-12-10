using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class UnityAudioPlayer : MonoBehaviour, IAudioPlayer
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public int Priority
        {
            get => _audioSource.priority;
            set => _audioSource.priority = value;
        }

        public bool IsLoop
        {
            get => _audioSource.loop;
            set => _audioSource.loop = value;
        }

        public bool IsMute
        {
            get => _audioSource.mute;
            set => _audioSource.mute = value;
        }

        public float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public void Pause()
        {
            _audioSource.Pause();
        }
    }
}