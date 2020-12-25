using System.Collections.Generic;
using System.Linq;

namespace Audio
{
    public sealed class AudioManager : IAudioManager
    {
        private readonly Dictionary<string, AudioCollection> _audioCollections = new Dictionary<string, AudioCollection>();
        private readonly Dictionary<string, IAudioPlayerController> _audioPlayerControllers = new Dictionary<string, IAudioPlayerController>();
        
        public IReadOnlyDictionary<string, IAudioCollection> AudioCollections => _audioCollections.ToDictionary(k => k.Key, v => (IAudioCollection) v.Value);

        public AudioManager(IAudioDatabase audioDatabase, IAudioSourcePool audioSourcePool)
        {
            InstantiateAudioPlayerControllers(audioDatabase, audioSourcePool);
            InstantiateAudioCollections(audioDatabase);
        }

        private void InstantiateAudioPlayerControllers(IAudioDatabase audioDatabase, IAudioSourcePool audioSourcePool)
        {
            foreach (var audioPlayerDescription in audioDatabase.AudioPlayerDescriptionDic.Values)
            {
                _audioPlayerControllers[audioPlayerDescription.Id] = new AudioPlayerController(audioPlayerDescription, audioSourcePool);
            }
        }

        private void InstantiateAudioCollections(IAudioDatabase audioDatabase)
        {
            foreach (var audioCollectionDescription in audioDatabase.AudioCollectionDescriptionDic.Values)
            {
                _audioCollections[audioCollectionDescription.Id] = new AudioCollection(audioCollectionDescription, _audioPlayerControllers);
            }
        }

        public IAudioStopper Play(string audioId, string collectionId, PlaySetting playSetting = new PlaySetting())
        {
            if (_audioCollections[collectionId].TryGetAudioPlayer(audioId, playSetting.AudioPriorityType, out var audioPlayer))
            {
                SetPlaySetting(playSetting, audioPlayer);

                audioPlayer.Play();
            }

            return new AudioStopper(audioPlayer);
        }

        private static void SetPlaySetting(PlaySetting playSetting, IAudioPlayer audioPlayer)
        {
            if (playSetting.ObjectToAttach != null)
            {
                audioPlayer.Attach(playSetting.ObjectToAttach);
            }

            if (playSetting.Position != null)
            {
                audioPlayer.SetPosition(playSetting.Position);
            }

            audioPlayer.FadeSeconds = playSetting.FadeSeconds;
        }
    }
}