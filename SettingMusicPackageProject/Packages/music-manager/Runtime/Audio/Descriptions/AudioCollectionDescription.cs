using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioCollectionDescription", menuName = "AudioPackage/AudioCollectionDescription", order = 0)]
    public sealed class AudioCollectionDescription : ScriptableObject, IAudioCollectionDescription
    {
        [SerializeField] private string _id;
        [SerializeField] private LimitAudioPriority _limitAudioPriority;

        private Dictionary<AudioPriorityType, int> _limitAudioPriorityDic;
        
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
    }
}