using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioPool))]
    public class AudioCollection : MonoBehaviour, IAudioCollection
    {
        [SerializeField] private protected string _name;
        [SerializeField] private protected List<AudioPlayerSetting> _audioPlayerList;

        private readonly IDictionary<string, int> _limitPlaySameAudioTogetherDic = new Dictionary<string, int>();
        private readonly IDictionary<string, int> _amountAudioPlayerPlayingDic = new Dictionary<string, int>();
        private readonly IDictionary<string, HashSet<IAudioPlayer>> _audioInGameDic = new Dictionary<string, HashSet<IAudioPlayer>>();
        private AudioPool _audioPool;
        private float _volumeAll;
        private bool _isMuteAll;

        public string Id => _name;

        private void Awake()
        {
            _audioPool = GetComponent<AudioPool>();
            _audioPool.AudioPoolConstructor(_audioPlayerList.Select(s => s.AudioPlayer));

            foreach (var audioPlayerSetting in _audioPlayerList)
            {
                SetLimitPlaySameAudioTogether(audioPlayerSetting.AudioPlayer.Id, audioPlayerSetting.AmountMaxPlayTogether);
                _audioInGameDic[audioPlayerSetting.AudioPlayer.Id] = new HashSet<IAudioPlayer>();
            }
        }

        public IAudioPlayer GetAudio(string idAudio)
        {
            var audioPlayer = _audioPool.Take(idAudio);
            AddAudioPlayerListener(audioPlayer);
            _audioInGameDic[idAudio].Add(audioPlayer);
            SetCollectionSettingToAudioPlayer(audioPlayer);

            return audioPlayer;
        }

        private void SetCollectionSettingToAudioPlayer(IAudioPlayer audioPlayer)
        {
            audioPlayer.IsMute = _isMuteAll;
            audioPlayer.Volume = _volumeAll;
        }

        private void AddAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.StartPlay += OnStartPlay;
            audioPlayer.FinishPlay += OnFinishPlay;
            audioPlayer.AudioDispose += OnAudioDispose;
        }

        private void RemoveAudioPlayerListener(IAudioPlayer audioPlayer)
        {
            audioPlayer.StartPlay -= OnStartPlay;
            audioPlayer.FinishPlay -= OnFinishPlay;
            audioPlayer.AudioDispose -= OnAudioDispose;
        }

        public void SetLimitPlaySameAudioTogether(string nameAudio, int maxAmountAudio = 2)
        {
            _limitPlaySameAudioTogetherDic[nameAudio] = maxAmountAudio;
        }

        public void PlayAll()
        {
            SetStateToAllAudioPlayersInGame(audioPlayer => audioPlayer.Play());
        }

        public void PauseAll()
        {
            SetStateToAllAudioPlayersInGame(audioPlayer => audioPlayer.Pause());
        }

        public void StopAll()
        {
            SetStateToAllAudioPlayersInGame(audioPlayer => audioPlayer.Stop());
        }

        public void MuteAll(bool isMute)
        {
            _isMuteAll = isMute;

            SetStateToAllAudioPlayersInGame(audioPlayer => audioPlayer.IsMute = isMute);
        }

        public void SetVolumeAll(float volume)
        {
            _volumeAll = volume;

            SetStateToAllAudioPlayersInGame(audioPlayer => audioPlayer.Volume = volume);
        }

        private void SetStateToAllAudioPlayersInGame(Action<IAudioPlayer> action)
        {
            foreach (var audioPlayerHashSet in _audioInGameDic.Values)
            {
                foreach (var audioPlayerExemplar in audioPlayerHashSet)
                {
                    action.Invoke(audioPlayerExemplar);
                }
            }
        }

        private void OnFinishPlay(IAudioPlayer audioPlayer)
        {
            _amountAudioPlayerPlayingDic[audioPlayer.Id] -= 1;
        }

        private void OnStartPlay(IAudioPlayer audioPlayer)
        {
            var maxAmountAudioInSameTime = _limitPlaySameAudioTogetherDic[audioPlayer.Id];
            var currentAmountAudioPlaying = _amountAudioPlayerPlayingDic[audioPlayer.Id];

            if (maxAmountAudioInSameTime > currentAmountAudioPlaying)
            {
                _amountAudioPlayerPlayingDic[audioPlayer.Id] += 1;
                (audioPlayer as IAudioCollectionControllable).AllowPlay(true);
            }
            else
            {
                (audioPlayer as IAudioCollectionControllable).AllowPlay(false);
            }
        }

        private void OnAudioDispose(IAudioPlayer audioPlayer)
        {
            RemoveAudioPlayerListener(audioPlayer);
            _audioInGameDic[audioPlayer.Id].Remove(audioPlayer);
            _audioPool.Return(audioPlayer);
        }

        [Serializable]
        protected struct AudioPlayerSetting
        {
            public AudioPlayer AudioPlayer;
            public int AmountMaxPlayTogether;
        }
    }
}