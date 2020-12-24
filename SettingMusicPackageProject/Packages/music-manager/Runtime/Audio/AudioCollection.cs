using System;
using System.Collections.Generic;

namespace Audio
{
    public sealed class AudioCollection : IAudioCollection
    {
        private readonly IAmountPriorityController _amountPriorityController;
        private readonly IDictionary<string, int> _audioPlayerByLimitPlay = new Dictionary<string, int>();
        private readonly IDictionary<string, HashSet<AudioPlayer>> _audioPlayerPlayingDic = new Dictionary<string, HashSet<AudioPlayer>>();
        private readonly IReadOnlyDictionary<string, IAudioPlayerDescription> _audioPlayerDescriptionDic;
        private readonly IDictionary<IAudioPlayer, IAudioSource> _audioSourceToReturnDic = new Dictionary<IAudioPlayer, IAudioSource>();
        private readonly IAudioSourcePool _audioSourcePool;
        private float _percentageVolume = 1;

        public string Id { get; }

        public AudioCollection(IAudioCollectionDescription audioCollectionDescription, IAudioSourcePool audioSourcePool)
        {
            var audioPlayerDescriptionDic = new Dictionary<string, IAudioPlayerDescription>();

            Id = audioCollectionDescription.Id;
            _audioSourcePool = audioSourcePool;
            _amountPriorityController = new AmountPriorityController(audioCollectionDescription.LimitAudioPriorityDic);

            foreach (var audioPlayerDescription in audioCollectionDescription.AudioPlayerDescriptionDic.Values)
            {
                _audioPlayerPlayingDic[audioPlayerDescription.Id] = new HashSet<AudioPlayer>();
                _audioPlayerByLimitPlay[audioPlayerDescription.Id] = audioPlayerDescription.LimitPlayTogether;
                audioPlayerDescriptionDic[audioPlayerDescription.Id] = audioPlayerDescription;
            }

            _audioPlayerDescriptionDic = audioPlayerDescriptionDic;
        }

        internal bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
        {
            bool isGetAudio;
            bool isAmountAudioPlayingLessThanLimit = _audioPlayerByLimitPlay[idAudio] > _audioPlayerPlayingDic[idAudio].Count;
            
            if (isAmountAudioPlayingLessThanLimit && _amountPriorityController.CheckSpaceAvailable(audioPriorityType))
            {
                audioPlayer = TakeAudioPlayerExemplar(idAudio, audioPriorityType);
                isGetAudio = true;
            }
            else
            {
                audioPlayer = null;
                isGetAudio = false;
            }

            return isGetAudio;
        }

        private IAudioPlayer TakeAudioPlayerExemplar(string idAudio, AudioPriorityType audioPriorityType)
        {
            var audioSource = _audioSourcePool.Take();
            var audioPlayer = new AudioPlayer(_audioPlayerDescriptionDic[idAudio], audioSource) {PercentageVolume = _percentageVolume};
            AddAudioPlayerListener(audioPlayer);
            _amountPriorityController.AddAudioPlayer(audioPriorityType, audioPlayer);
            _audioPlayerPlayingDic[idAudio].Add(audioPlayer);
            _audioSourceToReturnDic[audioPlayer] = audioSource;

            return audioPlayer;
        }

        private void ReturnAudioPlayerExemplar(IAudioPlayer audioPlayer)
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
            ReturnAudioPlayerExemplar(audioPlayer);
        }

        public void SetLimitPlaySameAudioTogether(string idAudio, int maxAmount)
        {
            _audioPlayerByLimitPlay[idAudio] = maxAmount;
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
            _percentageVolume = volume;
            ActToAllAudioPlayers(audioPlayer => audioPlayer.PercentageVolume = _percentageVolume);
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