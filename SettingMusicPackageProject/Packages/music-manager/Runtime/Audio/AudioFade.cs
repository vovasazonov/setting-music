using UnityEngine;

namespace Audio
{
    public class AudioFade : IAudioFade
    {
        private readonly AudioSource _audioSource;
        private float _originalVolume;
        private float _startVolume;
        private float _targetVolume;
        private float _currentSeconds;
        
        public bool IsFading { get; private set; }
        public float FadeSeconds { get; set; }

        public AudioFade(AudioSource audioSource)
        {
            _audioSource = audioSource;
            _originalVolume = audioSource.volume;
        }
        
        public void UpdateVolume(float volume)
        {
            _originalVolume = volume;
            _startVolume *= volume;
            _targetVolume *= volume;
            _audioSource.volume *= volume;
        }

        public void StartFadeIn()
        {
            IsFading = true;
            _currentSeconds = 0;
            _startVolume = 0;
            _targetVolume = _originalVolume;
        }

        public void StartFadeOut()
        {
            IsFading = true;
            _currentSeconds = 0;
            _startVolume = _originalVolume;
            _targetVolume = 0;
        }

        public void StopFade()
        {
            IsFading = false;
            _audioSource.volume = _originalVolume;
        }

        public void Update()
        {
            if (IsFading)
            {
                if (_currentSeconds < FadeSeconds)
                {
                    _currentSeconds += Time.deltaTime;
                    _audioSource.volume = Mathf.Lerp(_startVolume, _targetVolume, _currentSeconds / FadeSeconds);
                }
                else
                {
                    StopFade();
                }
            }
        }
    }
}