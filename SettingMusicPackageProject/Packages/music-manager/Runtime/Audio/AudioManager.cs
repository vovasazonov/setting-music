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
                var audioPlayerController = new AudioPlayerController(audioPlayerDescription, audioSourcePool);
                _audioPlayerControllers.Add(audioPlayerDescription.Id, audioPlayerController);
            }
        }

        private void InstantiateAudioCollections(IAudioDatabase audioDatabase)
        {
            foreach (var audioCollectionDescription in audioDatabase.AudioCollectionDescriptionDic.Values)
            {
                var audioCollection = new AudioCollection(audioCollectionDescription, _audioPlayerControllers);
                _audioCollections.Add(audioCollectionDescription.Id, audioCollection);
            }
        }
        
        public IAudioStopper Play(string audioId, string collectionId)
        {
            return Play(audioId, collectionId, PlaySetting.Default());
        }
        
        public IAudioStopper Play(string audioId, string collectionId, IPlaySetting playSetting)
        {
            IAudioStopper audioStopper;
            var audioCollection = _audioCollections[collectionId];
            
            if (audioCollection.TryGetAudioPlayer(audioId, playSetting.AudioPriorityType, out var audioPlayer))
            {
                SetPlaySetting(playSetting, audioPlayer);

                audioPlayer.Play();
                audioStopper = new AudioStopper(audioPlayer);
            }
            else
            {
                audioStopper = null;
            }

            return audioStopper;
        }

        private void SetPlaySetting(IPlaySetting playSetting, IAudioPlayer audioPlayer)
        {
            if (playSetting.CanFollowTransform)
            {
                audioPlayer.Attach(playSetting.FollowTransform);
            }

            if (playSetting.HasPosition)
            {
                audioPlayer.SetPosition(playSetting.Position);
            }

            if (playSetting.CanFade)
            {
                audioPlayer.FadeSeconds = playSetting.FadeSeconds;
            }
        }
    }
}