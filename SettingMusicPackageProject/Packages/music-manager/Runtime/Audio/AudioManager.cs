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

        public IAudioStopper Play(string audioId, string collectionId, IPlaySetting playSetting = null)
        {
            IAudioStopper audioStopper = null;
            playSetting = playSetting ?? new PlaySetting();
            
            if (_audioCollections[collectionId].TryGetAudioPlayer(audioId, playSetting.AudioPriorityType, out var audioPlayer))
            {
                SetPlaySetting(playSetting, audioPlayer);

                audioPlayer.Play();
                audioStopper = new AudioStopper(audioPlayer);
            }

            return audioStopper;
        }

        private void SetPlaySetting(IPlaySetting playSetting, IAudioPlayer audioPlayer)
        {
            if (playSetting.FollowTransform != null)
            {
                audioPlayer.Attach(playSetting.FollowTransform);
            }

            if (playSetting.Position != null)
            {
                audioPlayer.SetPosition(playSetting.Position);
            }

            if (playSetting.FadeSeconds != null)
            {
                audioPlayer.FadeSeconds = (float) playSetting.FadeSeconds;
            }
        }
    }
}