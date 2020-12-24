using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "AudioPackage/AudioDatabase", order = 0)]
    public sealed class AudioDatabase : ScriptableObject, IAudioDatabase
    {
        [SerializeField] private List<AudioCollectionDescription> _audioCollectionDescriptionList;
        [SerializeField] private LimitAudioPriority _limitAudioPriority;

        public IReadOnlyDictionary<AudioPriorityType, int> LimitPriorityPlayTogether { get; private set; }
        public IReadOnlyDictionary<string, IAudioCollectionDescription> AudioCollectionDescriptions { get; private set; }

        private void Awake()
        {
            LimitPriorityPlayTogether = _limitAudioPriority.ConvertToDictionary();
            AudioCollectionDescriptions = _audioCollectionDescriptionList.ToDictionary(k => k.Id, v => (IAudioCollectionDescription) v);
        }
    }
}