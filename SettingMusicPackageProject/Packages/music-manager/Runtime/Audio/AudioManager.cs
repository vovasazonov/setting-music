using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private protected List<AudioCollection> _musicCollectionList;
        [SerializeField] private protected List<AudioCollection> _soundCollectionList;
        [SerializeField] private protected int _limitPlayAudioTogether;

        private IReadOnlyDictionary<string, IAudioCollection> _musicCollectionDic;
        private IReadOnlyDictionary<string, IAudioCollection> _soundCollectionDic;
        private bool _isMuteMusic;
        private bool _isMuteSound;
        private float _musicVolume;
        private float _soundVolume;
        private readonly HashSet<IAudioPlayer> _audioPlayerPlayingHash = new HashSet<IAudioPlayer>();

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

        public int LimitPlayAudioTogether
        {
            get => _limitPlayAudioTogether;
            set => _limitPlayAudioTogether = value;
        }

        private void Awake()
        {
            _musicCollectionDic = _musicCollectionList.ToDictionary(k => k.Id, v => (IAudioCollection) v);
            _soundCollectionDic = _soundCollectionList.ToDictionary(k => k.Id, v => (IAudioCollection) v);

            _musicCollectionList.ForEach(AddAudioCollectionListener);
            _soundCollectionList.ForEach(AddAudioCollectionListener);
        }

        private void AddAudioCollectionListener(IAudioCollection audioCollection)
        {
            audioCollection.StartPlay += OnAudioPlayerInCollectionStartPlay;
        }

        private void RemoveAudioCollectionListener(IAudioCollection audioCollection)
        {
            audioCollection.StartPlay -= OnAudioPlayerInCollectionStartPlay;
        }

        private void AddAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.FinishPlay += OnAudioPlayerFinishPlay;
        }

        private void RemoveAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.FinishPlay -= OnAudioPlayerFinishPlay;
        }

        private void OnAudioPlayerFinishPlay(IAudioPlayer audioPlayer)
        {
            RemoveAudioPlayerListener(audioPlayer);
            _audioPlayerPlayingHash.Remove(audioPlayer);
        }

        private void OnAudioPlayerInCollectionStartPlay(IAudioPlayer audioPlayer, ref bool isAllowPlay)
        {
            isAllowPlay = false;

            var amountPriorityHigh = _audioPlayerPlayingHash.Count(a => a.PriorityType == AudioPriorityType.High);
            var amountPriorityMedium = _audioPlayerPlayingHash.Count(a => a.PriorityType == AudioPriorityType.Medium);
            var amountPriorityLow = _audioPlayerPlayingHash.Count(a => a.PriorityType == AudioPriorityType.Low);
            var amountWhole = amountPriorityHigh + amountPriorityMedium + amountPriorityLow;

            if (amountWhole < LimitPlayAudioTogether)
            {
                AllowAudioPlay(audioPlayer, out isAllowPlay);
            }
            else if (AudioPriorityType.High == audioPlayer.PriorityType)
            {
                if (amountPriorityLow > 0)
                {
                    StopOneAudioPlayer(AudioPriorityType.Low);
                    AllowAudioPlay(audioPlayer, out isAllowPlay);
                }
                else if (amountPriorityMedium > 0)
                {
                    StopOneAudioPlayer(AudioPriorityType.Medium);
                    AllowAudioPlay(audioPlayer, out isAllowPlay);
                }
            }
            else if (AudioPriorityType.Medium == audioPlayer.PriorityType)
            {
                if (amountPriorityLow > 0)
                {
                    StopOneAudioPlayer(AudioPriorityType.Low);
                    AllowAudioPlay(audioPlayer, out isAllowPlay);
                }
            }
        }

        private void AllowAudioPlay(IAudioPlayer audioPlayer, out bool isAllowPlay)
        {
            isAllowPlay = true;
            AddAudioPlayerListener(audioPlayer);
            _audioPlayerPlayingHash.Add(audioPlayer);
        }

        private void StopOneAudioPlayer(AudioPriorityType audioPriorityType)
        {
            var audioPlayerLow = _audioPlayerPlayingHash.First(a => a.PriorityType == audioPriorityType);
            audioPlayerLow.Stop();
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