using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioPool : MonoBehaviour, IAudioPool
    {
        [SerializeField] private protected AudioSource _audioSourcePrefab;
        private readonly Queue<AudioSource> _freeAudioSources = new Queue<AudioSource>();
        private readonly Dictionary<IAudioPlayer, AudioSource> _busyAudioSources;

        public void Init(IEnumerable<IAudioPlayerDescription> audioPlayerDescriptions)
        {
            // TODO: keep in this class all description and reference to clips.
            throw new NotImplementedException();
        }
        
        public IAudioPlayer Take(string idAudio)
        {
            if (_freeAudioSources.Count == 0)
            {
                InstantiateAudioSource();
            }

            var audioSource = _freeAudioSources.Dequeue();
            SetToReleaseSettings(audioSource);
            var audioPlayer = new AudioPlayer(idAudio, audioSource);
            
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
        }

        private void SetToReleaseSettings(AudioSource audioSource)
        {
            audioSource.enabled = true;
        }
    }
}