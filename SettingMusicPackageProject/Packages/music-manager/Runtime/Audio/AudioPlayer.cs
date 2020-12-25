namespace Audio
{
    public sealed class AudioPlayer : IAudioPlayer
    {
        public event DisposingHandler Disposing;
        
        private readonly IAudioSource _audioSource;
        private readonly float _originalVolume;
        private float _percentageVolume;
        
        public string Id { get; }

        public float FadeSeconds
        {
            set => _audioSource.FadeSeconds = value;
        }

        public bool IsMute
        {
            set => _audioSource.IsMute = value;
        }

        public float PercentageVolume
        {
            set
            {
                _percentageVolume = value;
                _audioSource.Volume = _originalVolume * _percentageVolume;
            }
        }

        public AudioPlayer(IAudioPlayerDescription audioPlayerDescription, IAudioSource audioSource)
        {
            _audioSource = audioSource;

            Id = audioPlayerDescription.Id;
            FadeSeconds = audioPlayerDescription.FadeSeconds;

            _originalVolume = audioPlayerDescription.Volume;
            _audioSource.SetClip(audioPlayerDescription.ClipId);
            _audioSource.FadeSeconds = audioPlayerDescription.FadeSeconds;
            _audioSource.IsLoop = audioPlayerDescription.IsLoop;
            _audioSource.Pitch = audioPlayerDescription.Pitch;
            _audioSource.SpatialBlend = audioPlayerDescription.SpatialBlend;
            _audioSource.StereoPan = audioPlayerDescription.StereoPan;
            _audioSource.Spread = audioPlayerDescription.Spread;
            _audioSource.DopplerLevel = audioPlayerDescription.DopplerLevel;
            _audioSource.MinDistance = audioPlayerDescription.MinDistance;
            _audioSource.MaxDistance = audioPlayerDescription.MaxDistance;
            _audioSource.RolloffMode = audioPlayerDescription.RolloffMode;
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public void SetPosition(IPosition position)
        {
            _audioSource.SetPosition(position);
        }

        public void Attach(object audioAttachableObject)
        {
            _audioSource.Attach(audioAttachableObject);
        }

        public void Dispose()
        {
            CallDisposing(this);
        }

        private void CallDisposing(IAudioPlayer audioPlayer)
        {
            Disposing?.Invoke(audioPlayer);
        }
    }
}