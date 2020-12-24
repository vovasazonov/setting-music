using Audio;

namespace Sample.Scripts
{
    public class ZombieModel : ICharacterModel
    {
        private readonly IAudioManager _audioManager;
        private readonly string _shoutZombieAudioId;
        private IAudioPlayer _audioPlayer;

        public ZombieModel(IAudioManager audioManager, string shoutZombieAudioId)
        {
            _audioManager = audioManager;
            _shoutZombieAudioId = shoutZombieAudioId;
        }

        public void HitMe()
        {
            _audioManager.Play(_shoutZombieAudioId);
        }
    }
}