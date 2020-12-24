using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioCollectionDescription", menuName = "AudioPackage/AudioCollectionDescription", order = 0)]
    public sealed class AudioCollectionDescription : ScriptableObject, IAudioCollectionDescription
    {
        [SerializeField] private string _id;
        [SerializeField] private List<AudioPlayerDescription> _audioPlayerDescriptions;
        [SerializeField] private LimitAudioPriority _limitAudioPriority;

        public string Id => _id;
        public IReadOnlyDictionary<AudioPriorityType, int> LimitAudioPriority { get; private set; }
        public IReadOnlyDictionary<string, IAudioPlayerDescription> AudioPlayerDescriptions { get; private set; }

        private void Awake()
        {
            LimitAudioPriority = _limitAudioPriority.ConvertToDictionary();
            AudioPlayerDescriptions = _audioPlayerDescriptions.ToDictionary(k => k.Id, v => (IAudioPlayerDescription) v);
        }
    }
}