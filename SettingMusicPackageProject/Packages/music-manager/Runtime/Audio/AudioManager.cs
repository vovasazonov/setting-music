using System;
using System.Collections.Generic;
using System.Linq;

namespace Audio
{
    public sealed class AudioManager : IAudioManager
    {
        private readonly IAmountPriorityAudioPlayerController _amountPriorityAudioPlayerController;
        private readonly IReadOnlyDictionary<string, string> _audioPlayerIdToAudioCollectionId;
        private readonly IReadOnlyDictionary<string, AudioCollection> _audioCollections;

        public IReadOnlyDictionary<string, IAudioCollection> AudioCollections => _audioCollections.ToDictionary(k => k.Key, v => (IAudioCollection) v.Value);

        public AudioManager()
        {
            throw new NotImplementedException();
            // TODO: get description and init values.
        }

        public bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
        {
            bool isGetAudio = false;

            if (_amountPriorityAudioPlayerController.FreeSpaceAvailable(audioPriorityType))
            {
                var idAudioCollection = _audioPlayerIdToAudioCollectionId[idAudio];
                isGetAudio = _audioCollections[idAudioCollection].TryGetAudioPlayer(idAudio, audioPriorityType, out audioPlayer);

                if (isGetAudio)
                {
                    AddAudioPlayerExemplar(audioPriorityType, audioPlayer);
                }
            }
            else
            {
                audioPlayer = null;
            }

            return isGetAudio;
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

        private void AddAudioPlayerExemplar(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer)
        {
            _amountPriorityAudioPlayerController.AddAudioPlayer(audioPriorityType, audioPlayer);
            AddAudioPlayerListener(audioPlayer);
        }

        private void RemoveAudioPlayerExemplar(IAudioPlayer audioPlayer)
        {
            _amountPriorityAudioPlayerController.RemoveAudioPlayer(audioPlayer);
            RemoveAudioPlayerListener(audioPlayer);
        }
    }
}