using Audio;

namespace Sample.Scripts
{
    public class CollectionVolumeModel : IVolumeModel
    {
        private readonly IAudioCollection _collection;
        private readonly float _stepVolume;
        private float _currentVolume = 1;

        public CollectionVolumeModel(IAudioCollection audioCollection, float stepVolume)
        {
            _collection = audioCollection;
            _stepVolume = stepVolume;
        }
        
        public void Increase()
        {
            _currentVolume += _stepVolume;
            _collection.SetVolumeAll(_currentVolume);
        }

        public void Decrease()
        {
            _currentVolume -= _stepVolume;
            _collection.SetVolumeAll(_currentVolume);
        }
    }
}