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
        }

        internal void Update()
        {
            throw new NotImplementedException();
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