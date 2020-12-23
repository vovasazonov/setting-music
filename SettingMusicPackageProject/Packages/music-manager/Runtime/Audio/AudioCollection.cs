using System;
using System.Collections.Generic;

namespace Audio
{
    public sealed class AudioCollection : IAudioCollection
    {
        private readonly IAmountPriorityController _amountPriorityController;
        private readonly IDictionary<string, int> _limitPlaySameAudioTogether = new Dictionary<string, int>();
        private readonly IDictionary<string, HashSet<AudioPlayer>> _audioPlayerPlayingDic = new Dictionary<string, HashSet<AudioPlayer>>();
        private readonly IReadOnlyDictionary<string, IAudioPlayerDescription> _audioPlayerDescriptions;
        private readonly IDictionary<IAudioPlayer, IAudioSource> _audioSourceToReturnDic = new Dictionary<IAudioPlayer, IAudioSource>();
        private readonly IAudioSourcePool _audioSourcePool;

        public string Id { get; }

        public AudioCollection(IAudioCollectionDescription audioCollectionDescription, IAudioSourcePool audioSourcePool)
        {
            var audioPlayerDescriptions = new Dictionary<string, IAudioPlayerDescription>();

            Id = audioCollectionDescription.Id;
            _audioSourcePool = audioSourcePool;
            _amountPriorityController = new AmountPriorityController(audioCollectionDescription.LimitPlayTogether);

            foreach (var audioPlayerDescription in audioCollectionDescription.AudioPlayerDescriptions)
            {
                _audioPlayerPlayingDic[audioPlayerDescription.Id] = new HashSet<AudioPlayer>();
                _limitPlaySameAudioTogether[audioPlayerDescription.Id] = audioPlayerDescription.LimitPlayTogether;
                audioPlayerDescriptions[audioPlayerDescription.Id] = audioPlayerDescription;
            }

            _audioPlayerDescriptions = audioPlayerDescriptions;
        }

        internal bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
        {
            bool isGetAudio;

            if (_limitPlaySameAudioTogether[idAudio] > _audioPlayerPlayingDic[idAudio].Count && _amountPriorityController.CheckSpaceAvailable(audioPriorityType))
            {
                AddAudioPlayerExemplar(idAudio, audioPriorityType, out audioPlayer);
                isGetAudio = true;
            }
            else
            {
                audioPlayer = null;
                isGetAudio = false;
            }

            return isGetAudio;
        }

        private void AddAudioPlayerExemplar(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
        {
            var audioSource = _audioSourcePool.Take();
            var audioPlayerExemplar = new AudioPlayer(_audioPlayerDescriptions[idAudio], audioSource);
            _amountPriorityController.AddAudioPlayer(audioPriorityType, audioPlayerExemplar);
            _audioPlayerPlayingDic[idAudio].Add(audioPlayerExemplar);
            _audioSourceToReturnDic[audioPlayerExemplar] = audioSource;
            audioPlayer = audioPlayerExemplar;
            AddAudioPlayerListener(audioPlayerExemplar);
        }

        private void RemoveAudioPlayerExemplar(IAudioPlayer audioPlayer)
        {
            _amountPriorityController.RemoveAudioPlayer(audioPlayer);
            _audioPlayerPlayingDic[audioPlayer.Id].Remove((AudioPlayer) audioPlayer);
            _audioSourcePool.Return(_audioSourceToReturnDic[audioPlayer]);
            _audioSourceToReturnDic.Remove(audioPlayer);
            RemoveAudioPlayerListener(audioPlayer);
        }

        private void AddAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.Disposing += OnAudioDisposing;
        }

        private void RemoveAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.Disposing -= OnAudioDisposing;
        }

        private void OnAudioDisposing(IAudioPlayer audioPlayer)
        {
            RemoveAudioPlayerExemplar(audioPlayer);
        }

        public void SetLimitPlaySameAudioTogether(string idAudio, int maxAmount)
        {
            _limitPlaySameAudioTogether[idAudio] = maxAmount;
        }

        public void PlayAll()
        {
            ActToAllAudioPlayers(audioPlayer => audioPlayer.Play());
        }

        public void StopAll()
        {
            ActToAllAudioPlayers(audioPlayer => audioPlayer.Stop());
        }

        public void MuteAll(bool isMute)
        {
            ActToAllAudioPlayers(audioPlayer => audioPlayer.IsMute = isMute);
        }

        public void SetVolumeAll(float volume)
        {
            ActToAllAudioPlayers(audioPlayer => audioPlayer.Volume = volume);
        }

        private void ActToAllAudioPlayers(Action<AudioPlayer> action)
        {
            foreach (var audioPlayerHashSet in _audioPlayerPlayingDic.Values)
            {
                foreach (var audioPlayerExemplar in audioPlayerHashSet)
                {
                    action.Invoke(audioPlayerExemplar);
                }
            }
        }
    }
}