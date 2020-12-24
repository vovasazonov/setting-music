using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioClipDescription", menuName = "AudioPackage/AudioClipDescription", order = 0)]
    public sealed class AudioClipDescription : ScriptableObject, IAudioClipDescription
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _audioClip;

        public string Id => _id;
        public AudioClip AudioClip => _audioClip;
    }
}