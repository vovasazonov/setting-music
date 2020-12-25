using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "AudioPackage/AudioDatabase", order = 0)]
    public sealed class AudioDatabase : ScriptableObject, IAudioDatabase
    {
        [SerializeField] private List<AudioCollectionDescription> _audioCollectionDescriptionList;
        [SerializeField] private List<AudioPlayerDescription> _audioPlayerDescriptionList;

        private Dictionary<string, IAudioCollectionDescription> _audioCollectionDescriptionDic;
        private Dictionary<string, IAudioPlayerDescription> _audioPlayerDescriptionDic;

        public IReadOnlyDictionary<string, IAudioCollectionDescription> AudioCollectionDescriptionDic
        {
            get
            {
                if (_audioCollectionDescriptionDic == null)
                {
                    _audioCollectionDescriptionDic = _audioCollectionDescriptionList.ToDictionary(k => k.Id, v => (IAudioCollectionDescription) v);
                }

                return _audioCollectionDescriptionDic;
            }
        }

        public IReadOnlyDictionary<string, IAudioPlayerDescription> AudioPlayerDescriptionDic
        {
            get
            {
                if (_audioPlayerDescriptionDic == null)
                {
                    _audioPlayerDescriptionDic = _audioPlayerDescriptionList.ToDictionary(k => k.Id, v => (IAudioPlayerDescription) v);
                }

                return _audioPlayerDescriptionDic;
            }
        }
    }
}