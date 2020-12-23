using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioCollectionDescription", menuName = "AudioPackage/AudioCollectionDescription", order = 0)]
    public class AudioCollectionDescription : ScriptableObject, IAudioCollectionDescription
    {
        [SerializeField] private protected string _id;
        [SerializeField] private protected List<AudioPlayerDescription> _audioDescriptions;
        [SerializeField] private protected int _limitImpotant;
        [SerializeField] private protected int _limitUnmpotant;
        [SerializeField] private protected int _limitLeast;

        public string Id => _id;
        public IReadOnlyDictionary<AudioPriorityType, int> LimitPlayTogether { get; private set; }
        public IEnumerable<IAudioPlayerDescription> AudioPlayerDescriptions => _audioDescriptions;

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