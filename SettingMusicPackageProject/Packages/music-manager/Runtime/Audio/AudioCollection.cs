using System;
using System.Collections.Generic;

namespace Audio
{
    public sealed class AudioCollection : IAudioCollection
    {
        private readonly IAmountPriorityController _amountPriorityController;
        private readonly IDictionary<string, int> _limitPlaySameAudioTogether = new Dictionary<string, int>();
        private readonly IDictionary<string, HashSet<IAudioPlayer>> _audioPlayerPlayingDic = new Dictionary<string, HashSet<IAudioPlayer>>();
        private readonly IAudioPool _audioPool;
        
        public string Id { get; }

        public AudioCollection(string id, IAudioCollectionDescription audioCollectionDescription, IAudioPool audioPool)
        {
            Id = id;
            _audioPool = audioPool;
            _amountPriorityController = new AmountPriorityController(audioCollectionDescription.LimitPlayTogether);
            
            foreach (var audioPlayerDescription in audioCollectionDescription.AudioPlayerDescriptions)
            {
                _audioPlayerPlayingDic[audioPlayerDescription.Id] = new HashSet<IAudioPlayer>();
                _limitPlaySameAudioTogether[audioPlayerDescription.Id] = audioPlayerDescription.LimitPlayTogether;
            }
        }

        public bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
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
            var audioPlayerExemplar = _audioPool.Take(idAudio);
            audioPlayer = audioPlayerExemplar;
            _amountPriorityController.AddAudioPlayer(audioPriorityType, audioPlayer);
            _audioPlayerPlayingDic[idAudio].Add(audioPlayerExemplar);
            AddAudioPlayerListener(audioPlayer);
        }

        private void RemoveAudioPlayerExemplar(IAudioPlayer audioPlayer)
        {
            _amountPriorityController.RemoveAudioPlayer(audioPlayer);
            _audioPlayerPlayingDic[audioPlayer.Id].Remove(audioPlayer);
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

        public void PauseAll()
        {
            ActToAllAudioPlayers(audioPlayer => audioPlayer.Pause());
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

        private void ActToAllAudioPlayers(Action<IAudioPlayer> action)
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