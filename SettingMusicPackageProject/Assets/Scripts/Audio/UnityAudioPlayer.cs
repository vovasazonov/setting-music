using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class UnityAudioPlayer : MonoBehaviour, IAudioPlayer
    {
        public event PlayHandler PlayHandler;
        
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

        public float Pitch
        {
            get => _audioSource.pitch;
            set => _audioSource.pitch = value;
        }

        public void Play()
        {
            OnPlayHandler(this);
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

        private void OnPlayHandler(IAudioPlayer audioPlayer)
        {
            PlayHandler?.Invoke(audioPlayer);
        }
    }
}