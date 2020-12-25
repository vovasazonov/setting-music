using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioCollectionDescription", menuName = "AudioPackage/AudioCollectionDescription", order = 0)]
    public sealed class AudioCollectionDescription : ScriptableObject, IAudioCollectionDescription
    {
        [SerializeField] private string _id;
        [SerializeField] private LimitAudioPriority _limitAudioPriority;
        [SerializeField] private float _volume = 1;

        private Dictionary<AudioPriorityType, int> _limitAudioPriorityDic;
        
        public string Id => _id;
        public float Volume => _volume;

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