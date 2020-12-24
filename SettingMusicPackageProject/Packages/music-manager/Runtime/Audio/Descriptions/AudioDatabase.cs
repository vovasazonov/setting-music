using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "AudioPackage/AudioDatabase", order = 0)]
    public sealed class AudioDatabase : ScriptableObject, IAudioDatabase
    {
        [SerializeField] private List<AudioCollectionDescription> _audioCollectionDescriptions;
        [SerializeField] private LimitAudioPriority _limitAudioPriority;

        private Dictionary<AudioPriorityType, int> _limitAudioPriorityDic;
        private Dictionary<string, IAudioCollectionDescription> _audioCollectionDescriptionDic;

        public IReadOnlyDictionary<AudioPriorityType, int> LimitAudioPriorityDic
        {
            get
            {
                if (_limitAudioPriorityDic == null)
                {
                    _limitAudioPriorityDic = _limitAudioPriority.ConvertToDictionary();
                }

                return _limitAudioPriorityDic;
            }
        }
        
        public IReadOnlyDictionary<string, IAudioCollectionDescription> AudioCollectionDescriptionDic
        {
            get
            {
                if (_audioCollectionDescriptionDic == null)
                {
                    _audioCollectionDescriptionDic = _audioCollectionDescriptions.ToDictionary(k => k.Id, v => (IAudioCollectionDescription) v);
                }

                return _audioCollectionDescriptionDic;
            }
        }
    }
}