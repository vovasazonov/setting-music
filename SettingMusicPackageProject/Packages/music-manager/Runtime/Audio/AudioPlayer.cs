using System;

namespace Audio
{
    public sealed class AudioPlayer : IAudioPlayer
    {
        private readonly IAudioSource _audioSource;
        public event DisposingHandler Disposing;

        public string Id { get; }

        public float FadeSeconds { get; set; }

        public bool IsMute
        {
            get => _audioSource.IsMute;
            set => _audioSource.IsMute = value;
        }

        public float Volume
        {
            get => _audioSource.Volume;
            set => _audioSource.Volume = value;
        }

        public AudioPlayer(IAudioPlayerDescription audioPlayerDescription, IAudioSource audioSource)
        {
            _audioSource = audioSource;

            Id = audioPlayerDescription.Id;
            FadeSeconds = audioPlayerDescription.FadeSeconds;
            
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

        internal void Update()
        {
            throw new NotImplementedException();
            // TODO: Fade realization.
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