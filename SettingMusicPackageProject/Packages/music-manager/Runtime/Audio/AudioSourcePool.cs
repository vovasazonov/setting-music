using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public sealed class AudioSourcePool : MonoBehaviour, IAudioSourcePool
    {
        [SerializeField] private AudioSourceView _audioSourcePrefab;
        private readonly Queue<IAudioSource> _freeAudioSources = new Queue<IAudioSource>();
        private IReadOnlyDictionary<string, AudioClip> _audioClips;

        public void Init(IReadOnlyDictionary<string, AudioClip> audioClips)
        {
            _audioClips = audioClips;
        }

        public IAudioSource Take()
        {
            if (_freeAudioSources.Count == 0)
            {
                InstantiateAudioSource();
            }

            var audioSource = _freeAudioSources.Dequeue();
            SetToReleaseSettings(audioSource);

            return audioSource;
        }

        public void Return(IAudioSource audioSource)
        {
            SetToFactorySettings(audioSource);
            _freeAudioSources.Enqueue(audioSource);
        }

        private void InstantiateAudioSource()
        {
            var exemplar = Instantiate(_audioSourcePrefab);
            exemplar.Init(_audioClips);
            Return(exemplar);
        }

        private void SetToFactorySettings(IAudioSource audioSource)
        {
            audioSource.SetEnable(false);
            audioSource.Attach(transform);
        }

        private void SetToReleaseSettings(IAudioSource audioSource)
        {
            audioSource.SetEnable(true);
        }
    }
}