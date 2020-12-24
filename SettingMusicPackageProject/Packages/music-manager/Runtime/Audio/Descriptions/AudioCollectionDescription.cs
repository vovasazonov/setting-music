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

        private Dictionary<AudioPriorityType, int> _limitAudioPriorityDic;
        private Dictionary<string, IAudioPlayerDescription> _audioPlayerDescriptionDic;
        
        public string Id => _id;

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

        public IReadOnlyDictionary<string, IAudioPlayerDescription> AudioPlayerDescriptionDic
        {
            get
            {
                if (_audioPlayerDescriptionDic == null)
                {
                    _audioPlayerDescriptionDic = _audioPlayerDescriptions.ToDictionary(k => k.Id, v => (IAudioPlayerDescription) v);
                }

                return _audioPlayerDescriptionDic;
            }
        }
    }
}