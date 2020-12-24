using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioClipDatabase", menuName = "AudioPackage/AudioClipDatabase", order = 0)]
    public sealed class AudioClipDatabase : ScriptableObject, IAudioClipDatabase
    {
        [SerializeField] private List<AudioClipDescription> _audioClipDescriptions;
        
        public IReadOnlyDictionary<string, AudioClip> AudioClips { get; private set; }

        private void Awake()
        {
            AudioClips = _audioClipDescriptions.ToDictionary(k => k.Id, v => v.AudioClip);
        }
    }
}