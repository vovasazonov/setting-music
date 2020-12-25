using Audio;

namespace Sample.Scripts
{
    public class ZombieModel : ICharacterModel
    {
        private readonly IAudioManager _audioManager;
        private readonly string _shoutZombieAudioId;
        private readonly string _audioCollectionId;
        private IAudioPlayer _audioPlayer;

        public ZombieModel(IAudioManager audioManager, string shoutZombieAudioId, string audioCollectionId)
        {
            _audioManager = audioManager;
            _shoutZombieAudioId = shoutZombieAudioId;
            _audioCollectionId = audioCollectionId;
        }

        public void HitMe()
        {
            _audioManager.Play(_shoutZombieAudioId, _audioCollectionId, new PlaySetting(fadeSeconds:2));
        }
    }
}