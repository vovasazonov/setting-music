using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioClipDatabase", menuName = "AudioPackage/AudioClipDatabase", order = 0)]
    public sealed class AudioClipDatabase : ScriptableObject, IAudioClipDatabase
    {
        [SerializeField] private List<AudioClipDescription> _audioClipDescriptions;

        private Dictionary<string, AudioClip> _audioClipDic;

        public IReadOnlyDictionary<string, AudioClip> AudioClipDic
        {
            get
            {
                if (_audioClipDic == null)
                {
                    _audioClipDic = _audioClipDescriptions.ToDictionary(k => k.Id, v => v.AudioClip);
                }

                return _audioClipDic;
            }
        }
    }
}