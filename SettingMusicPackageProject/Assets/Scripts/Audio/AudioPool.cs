using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public class AudioPool : MonoBehaviour, IAudioPool
    {
        private Dictionary<string, AudioPlayer> _audioPrefabs;
        private readonly Dictionary<string, HashSet<AudioPlayer>> _audioFree = new Dictionary<string, HashSet<AudioPlayer>>();
        private readonly Dictionary<string, HashSet<AudioPlayer>> _audioBusy = new Dictionary<string, HashSet<AudioPlayer>>();

        public void AudioPoolConstructor(IEnumerable<AudioPlayer> audioPlayers)
        {
            _audioPrefabs = audioPlayers.ToDictionary(k => k.Id, v => v);
            
            foreach (var audioPlayer in audioPlayers)
            {
                _audioFree.Add(audioPlayer.Id, new HashSet<AudioPlayer>());
                _audioBusy.Add(audioPlayer.Id, new HashSet<AudioPlayer>());
            }
        }

        public IAudioPlayer Take(string idAudio)
        {
            var freeAudioPlayers = _audioFree[idAudio];
            var busyAudioPlayers = _audioBusy[idAudio];

            if (freeAudioPlayers.Count == 0)
            {
                InstantiateAudioPlayer(idAudio);
            }

            var audioPlayerTaking = freeAudioPlayers.First();
            freeAudioPlayers.Remove(audioPlayerTaking);
            busyAudioPlayers.Add(audioPlayerTaking);
            SetToReleaseSettings(audioPlayerTaking);

            return audioPlayerTaking;
        }

        public void Return(IAudioPlayer audioPlayer)
        {
            var freeAudioPlayers = _audioFree[audioPlayer.Id];
            var busyAudioPlayers = _audioBusy[audioPlayer.Id];
            var audioPlayerExemplar = audioPlayer as AudioPlayer;

            busyAudioPlayers.Remove(audioPlayerExemplar);
            SetToFactorySettings(audioPlayerExemplar);
            freeAudioPlayers.Add(audioPlayerExemplar);
        }

        private void InstantiateAudioPlayer(string idAudio)
        {
            var prefab = _audioPrefabs[idAudio];
            var exemplar = Instantiate(prefab);
            
            SetToFactorySettings(exemplar);
            _audioFree[idAudio].Add(exemplar);
        }

        private void SetToFactorySettings(AudioPlayer audioPlayer)
        {
            audioPlayer.enabled = false;
            audioPlayer.transform.SetParent(transform);
        }
        
        private void SetToReleaseSettings(AudioPlayer audioPlayer)
        {
            audioPlayer.enabled = true;
        }
    }
}