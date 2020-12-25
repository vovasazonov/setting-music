using System.Collections.Generic;
using System.Linq;

namespace Audio
{
    public sealed class AudioManager : IAudioManager
    {
        private readonly IDictionary<string, string> _playerIdByCollectionId = new Dictionary<string, string>();
        private readonly IDictionary<string, AudioCollection> _audioCollections = new Dictionary<string, AudioCollection>();

        public IReadOnlyDictionary<string, IAudioCollection> AudioCollections => _audioCollections.ToDictionary(k => k.Key, v => (IAudioCollection) v.Value);

        public AudioManager(IAudioDatabase audioDatabase, IAudioSourcePool audioSourcePool)
        {
            foreach (var audioCollectionDescription in audioDatabase.AudioCollectionDescriptionDic.Values)
            {
                _audioCollections[audioCollectionDescription.Id] = new AudioCollection(audioCollectionDescription, audioSourcePool);
                SetPlayerIdBelongCollectionId(audioCollectionDescription);
            }
        }

        private void SetPlayerIdBelongCollectionId(IAudioCollectionDescription collectionDescription)
        {
            foreach (var playerDescription in collectionDescription.AudioPlayerDescriptionDic.Values)
            {
                _playerIdByCollectionId[playerDescription.Id] = collectionDescription.Id;
            }
        }

        public IAudioPlayer Play(string idAudio, PlaySetting playSetting = new PlaySetting())
        {
            if (TryGetAudioPlayer(idAudio, playSetting.AudioPriorityType, out var audioPlayer))
            { 
                if (playSetting.ObjectToAttach != null)
                {
                    audioPlayer.Attach(playSetting.ObjectToAttach);
                }
                audioPlayer.FadeSeconds = playSetting.FadeSeconds;
                playSetting.Position = playSetting.Position;
                
                audioPlayer.Play();
            }

            return audioPlayer;
        }

        private bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer)
        {
            var idAudioCollection = _playerIdByCollectionId[idAudio];
            bool isGetAudio = _audioCollections[idAudioCollection].TryGetAudioPlayer(idAudio, audioPriorityType, out audioPlayer);

            return isGetAudio;
        }
    }
}