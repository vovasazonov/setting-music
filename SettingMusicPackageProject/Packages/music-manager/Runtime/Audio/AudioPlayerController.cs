using System.Collections.Generic;

namespace Audio
{
    internal sealed class AudioPlayerController : IAudioPlayerController
    {
        private readonly IAudioPlayerDescription _audioPlayerDescription;
        private readonly IAudioSourcePool _audioSourcePool;
        private readonly HashSet<IAudioPlayer> _playingAudioPlayers = new HashSet<IAudioPlayer>();
        private readonly IDictionary<IAudioPlayer, IAudioSource> _toReturnAudioSources = new Dictionary<IAudioPlayer, IAudioSource>();

        public AudioPlayerController(IAudioPlayerDescription audioPlayerDescription, IAudioSourcePool audioSourcePool)
        {
            _audioPlayerDescription = audioPlayerDescription;
            _audioSourcePool = audioSourcePool;
        }

        public bool IsAmountPlayingLessLimit()
        {
            return _audioPlayerDescription.LimitPlayTogether > _playingAudioPlayers.Count;
        }

        public IAudioPlayer GetAudioPlayer()
        {
            var audioPlayer = TakeAudioFromPool();
            AddAudioPlayerListener(audioPlayer);
            _playingAudioPlayers.Add(audioPlayer);

            return audioPlayer;
        }

        private void AddAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.Disposing += OnDisposing;
        }

        private void RemoveAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.Disposing -= OnDisposing;
        }

        private void OnDisposing(IAudioPlayer audioPlayer)
        {
            RemoveAudioPlayerListener(audioPlayer);
            ReturnAudioToPool(audioPlayer);
            _playingAudioPlayers.Remove(audioPlayer);
        }

        private AudioPlayer TakeAudioFromPool()
        {
            var audioSource = _audioSourcePool.Take();
            var audioPlayer = new AudioPlayer(_audioPlayerDescription, audioSource);
            _toReturnAudioSources.Add(audioPlayer, audioSource);
            return audioPlayer;
        }
        
        private void ReturnAudioToPool(IAudioPlayer audioPlayer)
        {
            var toReturnAudioSource = _toReturnAudioSources[audioPlayer];
            _audioSourcePool.Return(toReturnAudioSource);
            _toReturnAudioSources.Remove(audioPlayer);
        }
    }
}