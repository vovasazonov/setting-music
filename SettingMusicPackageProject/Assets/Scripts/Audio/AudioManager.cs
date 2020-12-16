using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private protected List<AudioCollection> _musicCollectionList;
        [SerializeField] private protected List<AudioCollection> _soundCollectionList;

        private IReadOnlyDictionary<string, IAudioCollection> _musicCollectionDic;
        private IReadOnlyDictionary<string, IAudioCollection> _soundCollectionDic;
        private bool _isMuteMusic;
        private bool _isMuteSound;
        private float _musicVolume;
        private float _soundVolume;

        public IReadOnlyDictionary<string, IAudioCollection> MusicCollections => _musicCollectionDic;
        public IReadOnlyDictionary<string, IAudioCollection> SoundCollections => _soundCollectionDic;

        public bool IsMuteMusic
        {
            get => _isMuteMusic;
            set
            {
                _isMuteMusic = value;
                MuteAudioCollections(_musicCollectionDic.Values, value);
            }
        }

        public bool IsMuteSound
        {
            get => _isMuteSound;
            set
            {
                _isMuteSound = value;
                MuteAudioCollections(_soundCollectionDic.Values, value);
            }
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                SetVolumeAudioCollections(_musicCollectionDic.Values, value);
            }
        }

        public float SoundVolume
        {
            get => _soundVolume;
            set
            {
                _soundVolume = value;
                SetVolumeAudioCollections(_soundCollectionDic.Values, value);
            }
        }

        private void Awake()
        {
            _musicCollectionDic = _musicCollectionList.ToDictionary(k => k.Id, v => (IAudioCollection) v);
            _soundCollectionDic = _soundCollectionList.ToDictionary(k => k.Id, v => (IAudioCollection) v);
        }

        private void MuteAudioCollections(IEnumerable<IAudioCollection> audioCollections, bool isMute)
        {
            foreach (var audioCollection in audioCollections)
            {
                audioCollection.MuteAll(isMute);
            }
        }

        private void SetVolumeAudioCollections(IEnumerable<IAudioCollection> audioCollections, float volume)
        {
            foreach (var audioCollection in audioCollections)
            {
                audioCollection.SetVolumeAll(volume);
            }
        }
    }
}