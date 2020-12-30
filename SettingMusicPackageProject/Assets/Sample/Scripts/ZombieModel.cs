using Audio;

namespace Sample.Scripts
{
    public class ZombieModel : ICharacterModel
    {
        private readonly IAudioManager _audioManager;
        private readonly string _shoutZombieAudioId;
        private readonly string _audioCollectionId;
        private readonly PlaySetting _playSetting;

        public ZombieModel(IAudioManager audioManager, string shoutZombieAudioId, string audioCollectionId, PlaySetting playSetting)
        {
            _audioManager = audioManager;
            _shoutZombieAudioId = shoutZombieAudioId;
            _audioCollectionId = audioCollectionId;
            _playSetting = playSetting;
        }

        public void HitMe()
        {
            _audioManager.Play(_shoutZombieAudioId, _audioCollectionId, _playSetting);
        }
    }
}