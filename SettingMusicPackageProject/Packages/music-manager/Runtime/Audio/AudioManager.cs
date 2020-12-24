using System.Collections.Generic;
using System.Linq;

namespace Audio
{
    public sealed class AudioManager : IAudioManager
    {
        private readonly IAmountPriorityController _amountPriorityController;
        private readonly IDictionary<string, string> _playerIdByCollectionId = new Dictionary<string, string>();
        private readonly IDictionary<string, AudioCollection> _audioCollections = new Dictionary<string, AudioCollection>();

        public IReadOnlyDictionary<string, IAudioCollection> AudioCollections => _audioCollections.ToDictionary(k => k.Key, v => (IAudioCollection) v.Value);

        public AudioManager(IAudioDatabase audioDatabase, IAudioSourcePool audioSourcePool)
        {
            _amountPriorityController = new AmountPriorityController(audioDatabase.LimitAudioPriorityDic);

            foreach (var audioCollectionDescription in audioDatabase.AudioCollectionDescriptionDic.Values)
            {
                _audioCollections[audioCollectionDescription.Id] = new AudioCollection(audioCollectionDescription, audioSourcePool);

                foreach (var audioPlayerDescription in audioCollectionDescription.AudioPlayerDescriptionDic.Values)
                {
                    _playerIdByCollectionId[audioPlayerDescription.Id] = audioCollectionDescription.Id;
                }
            }
        }

        public bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
        {
            bool isGetAudio = false;

            if (_amountPriorityController.CheckSpaceAvailable(audioPriorityType))
            {
                var idAudioCollection = _playerIdByCollectionId[idAudio];
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
            _amountPriorityController.AddAudioPlayer(audioPriorityType, audioPlayer);
            AddAudioPlayerListener(audioPlayer);
        }

        private void RemoveAudioPlayerExemplar(IAudioPlayer audioPlayer)
        {
            _amountPriorityController.RemoveAudioPlayer(audioPlayer);
            RemoveAudioPlayerListener(audioPlayer);
        }
    }
}