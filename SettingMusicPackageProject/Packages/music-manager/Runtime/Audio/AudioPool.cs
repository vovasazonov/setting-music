using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public sealed class AudioPool : MonoBehaviour, IAudioPool
    {
        [SerializeField] private AudioSource _audioSourcePrefab;
        private readonly Queue<AudioSource> _freeAudioSources = new Queue<AudioSource>();
        private readonly Dictionary<IAudioPlayer, AudioSource> _busyAudioSources;
        private IReadOnlyDictionary<string, IAudioPlayerDescription> _audioPlayerDescriptions;

        public void Init(IEnumerable<IAudioPlayerDescription> audioPlayerDescriptions)
        {
            _audioPlayerDescriptions = audioPlayerDescriptions.ToDictionary(k => k.Id, v => v);
        }

        public IAudioPlayer Take(string idAudio)
        {
            if (_freeAudioSources.Count == 0)
            {
                InstantiateAudioSource();
            }

            var audioSource = _freeAudioSources.Dequeue();
            SetToReleaseSettings(audioSource);
            var audioPlayer = new AudioPlayer(_audioPlayerDescriptions[idAudio], audioSource);

            return audioPlayer;
        }

        private void InstantiateAudioSource()
        {
            var exemplar = Instantiate(_audioSourcePrefab);

            SetToFactorySettings(exemplar);
            _freeAudioSources.Enqueue(exemplar);
        }

        private void SetToFactorySettings(AudioSource audioSource)
        {
            audioSource.enabled = false;
            audioSource.transform.SetParent(transform);
            audioSource.clip = null;
        }

        private void SetToReleaseSettings(AudioSource audioSource)
        {
            audioSource.enabled = true;
        }
    }
}