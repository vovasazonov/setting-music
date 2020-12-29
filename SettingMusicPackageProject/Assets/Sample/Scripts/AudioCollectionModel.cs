using Audio;

namespace Sample.Scripts
{
    public class AudioCollectionModel : IAudioCollectionModel
    {
        private readonly IAudioCollection _collection;
        private readonly float _stepVolume;
        private float _currentVolume = 1;
        private bool _isMute;

        public AudioCollectionModel(IAudioCollection audioCollection, float stepVolume)
        {
            _collection = audioCollection;
            _stepVolume = stepVolume;
        }
        
        public void IncreaseVolume()
        {
            _currentVolume += _stepVolume;
            _collection.SetVolumeAll(_currentVolume);
        }

        public void DecreaseVolume()
        {
            _currentVolume -= _stepVolume;
            _collection.SetVolumeAll(_currentVolume);
        }

        public void Mute()
        {
            _isMute = !_isMute;
            _collection.MuteAll(_isMute);
        }

        public void Stop()
        {
            _collection.StopAll();
        }
    }
}