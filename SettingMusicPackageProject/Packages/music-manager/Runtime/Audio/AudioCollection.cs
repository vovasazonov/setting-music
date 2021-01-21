using System;
using System.Collections.Generic;

namespace Audio
{
    internal sealed class AudioCollection : IAudioCollection
    {
        private readonly IReadOnlyDictionary<string, IAudioPlayerController> _audioPlayerControllers;
        private readonly IAmountPriorityController _amountPriorityController;
        private readonly HashSet<IAudioPlayer> _playingAudioPlayers = new HashSet<IAudioPlayer>();
        private float _volume;
        private bool _isMute;

        public string Id { get; }

        internal AudioCollection(IAudioCollectionDescription audioCollectionDescription, IReadOnlyDictionary<string, IAudioPlayerController> audioPlayerControllers)
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
            var audioPlayerController = _audioPlayerControllers[idAudio];
            
            if (audioPlayerController.IsAmountPlayingLessLimit())
            {
                if (_amountPriorityController.CheckSpaceAvailable(audioPriorityType))
                {
                    isGetAudio = true;
                    audioPlayer = audioPlayerController.GetAudioPlayer();
                    AddAudioPlayer(audioPlayer, audioPriorityType);
                }
            }

            return isGetAudio;
        }

        private void AddAudioPlayer(IAudioPlayer audioPlayer, AudioPriorityType audioPriorityType)
        {
            audioPlayer.Volume = _volume;
            audioPlayer.IsMute = _isMute;
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

        public void StopAll()
        {
            ActToAllAudioPlayers(audioPlayer => audioPlayer.Stop());
        }

        public void MuteAll(bool isMute)
        {
            _isMute = isMute;
            ActToAllAudioPlayers(audioPlayer => audioPlayer.IsMute = isMute);
        }

        public void SetVolumeAll(float volume)
        {
            _volume = volume;
            ActToAllAudioPlayers(audioPlayer => audioPlayer.Volume = _volume);
        }

        private void ActToAllAudioPlayers(Action<IAudioPlayer> action)
        {
            List<IAudioPlayer> audioPlayers = new List<IAudioPlayer>(_playingAudioPlayers);
            foreach (var audioPlayer in audioPlayers)
            {
                action.Invoke(audioPlayer);
            }
        }
    }
}