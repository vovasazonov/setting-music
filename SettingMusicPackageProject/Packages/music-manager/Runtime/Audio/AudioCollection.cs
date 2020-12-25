using System;
using System.Collections.Generic;

namespace Audio
{
    public sealed class AudioCollection : IAudioCollection
    {
        private readonly IReadOnlyDictionary<string, IAudioPlayerController> _audioPlayerControllers;
        private readonly IAmountPriorityController _amountPriorityController;
        private readonly HashSet<IAudioPlayer> _playingAudioPlayers = new HashSet<IAudioPlayer>();
        private float _volume;

        public string Id { get; }

        public AudioCollection(IAudioCollectionDescription audioCollectionDescription, IReadOnlyDictionary<string, IAudioPlayerController> audioPlayerControllers)
        {
            Id = audioCollectionDescription.Id;
            _volume = audioCollectionDescription.Volume;
            
            _audioPlayerControllers = audioPlayerControllers;
            _amountPriorityController = new AmountPriorityController(audioCollectionDescription.LimitAudioPriorityDic);
        }

        internal bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
        {
            bool isGetAudio = false;
            audioPlayer = null;

            if (_audioPlayerControllers[idAudio].IsAmountPlayingLessLimit())
            {
                if (_amountPriorityController.CheckSpaceAvailable(audioPriorityType))
                {
                    isGetAudio = true;
                    audioPlayer = _audioPlayerControllers[idAudio].GetAudioPlayer();
                    AddAudioPlayer(audioPlayer, audioPriorityType);
                }
            }

            return isGetAudio;
        }

        private void AddAudioPlayer(IAudioPlayer audioPlayer, AudioPriorityType audioPriorityType)
        {
            audioPlayer.Volume = _volume;
            _amountPriorityController.AddAudioPlayer(audioPriorityType, audioPlayer);
            AddAudioPlayerListener(audioPlayer);
            _playingAudioPlayers.Add(audioPlayer);
        }

        private void RemoveAudioPlayer(IAudioPlayer audioPlayer)
        {
            _amountPriorityController.RemoveAudioPlayer(audioPlayer);
            _playingAudioPlayers.Remove(audioPlayer);
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
            RemoveAudioPlayer(audioPlayer);
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
            _volume = volume;
            ActToAllAudioPlayers(audioPlayer => audioPlayer.Volume = _volume);
        }

        private void ActToAllAudioPlayers(Action<IAudioPlayer> action)
        {
            foreach (var playingAudioPlayer in _playingAudioPlayers)
            {
                action.Invoke(playingAudioPlayer);
            }
        }
    }
}