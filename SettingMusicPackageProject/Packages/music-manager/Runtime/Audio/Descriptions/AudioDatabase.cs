using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "AudioPackage/AudioDatabase", order = 0)]
    public class AudioDatabase : ScriptableObject, IAudioDatabase
    {
        [SerializeField] private protected List<AudioCollectionDescription> audioCollectionDescriptions;
        [SerializeField] private protected int _limitImpotant;
        [SerializeField] private protected int _limitUnmpotant;
        [SerializeField] private protected int _limitLeast;
        
        public IReadOnlyDictionary<AudioPriorityType, int> LimitPlayTogether { get; private set; }
        public IEnumerable<IAudioCollectionDescription> AudioCollectionDescriptions => audioCollectionDescriptions;
        
        private void Awake()
        {
            LimitPlayTogether = new Dictionary<AudioPriorityType, int>
            {
                {AudioPriorityType.Important, _limitImpotant},
                {AudioPriorityType.Unimportant, _limitUnmpotant},
                {AudioPriorityType.Least, _limitLeast}
            };
        }
    }
}