namespace Audio
{
    internal sealed class AudioFade : IAudioFade
    {
        private readonly IVolume _volume;
        private float _originalVolume;
        private float _startVolume;
        private float _targetVolume;
        private float _currentSeconds;
        
        public bool IsFading { get; private set; }
        public float FadeSeconds { get; set; }

        public AudioFade(IVolume volume)
        {
            _volume = volume;
            _originalVolume = _volume.Volume;
        }
        
        public void UpdateVolume(float volume)
        {
            _originalVolume = volume;
            _startVolume *= volume;
            _targetVolume *= volume;
            _volume.Volume *= volume;
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
            _volume.Volume = _originalVolume;
        }

        public void Update(float deltaTime)
        {
            if (IsFading)
            {
                if (_currentSeconds < FadeSeconds)
                {
                    _currentSeconds += deltaTime;
                    _volume.Volume = Lerp(_startVolume, _targetVolume, _currentSeconds / FadeSeconds);
                }
                else
                {
                    StopFade();
                }
            }
        }
        
        private float Lerp(float start, float end, float interpolationBetweenStartEnd)
        {
            return start * (1 - interpolationBetweenStartEnd) + end * interpolationBetweenStartEnd;
        }
    }
}