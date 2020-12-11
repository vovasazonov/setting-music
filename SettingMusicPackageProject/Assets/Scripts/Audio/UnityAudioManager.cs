using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class UnityAudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private protected List<UnityAudioPlayer> _inInspectorMusics;
        [SerializeField] private protected List<UnityAudioPlayer> _inInspectorSounds;
        [SerializeField] private protected bool _isMuteSound;
        [SerializeField] private protected bool _isMuteMusic;
        [SerializeField, Range(0, 1)] private protected float _soundVolume;
        [SerializeField, Range(0, 1)] private protected float _musicVolume;

        private readonly HashSet<IAudioPlayer> _soundPlayers = new HashSet<IAudioPlayer>();
        private readonly HashSet<IAudioPlayer> _musicPlayers = new HashSet<IAudioPlayer>();

        private void Awake()
        {
            _inInspectorMusics.ForEach(AddMusic);
            _inInspectorSounds.ForEach(AddSound);
        }
        
        private void OnValidate()
        {
            IsMuteSound = _isMuteSound;
            IsMuteMusic = _isMuteMusic;
            SoundVolume = _soundVolume;
            MusicVolume = _musicVolume;
        }

        public bool IsMuteSound
        {
            get => _isMuteSound;
            set => MuteSound(value);
        }

        public bool IsMuteMusic
        {
            get => _isMuteMusic;
            set => MuteMusic(value);
        }

        public float SoundVolume
        {
            get => _soundVolume;
            set => SetSoundVolume(value);
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set => SetMusicVolume(value);
        }

        private void MuteSound(bool isMute)
        {
            foreach (var soundPlayer in _soundPlayers)
            {
                soundPlayer.IsMute = isMute;
            }
        }
        
        private void MuteMusic(bool isMute)
        {
            foreach (var musicPlayer in _musicPlayers)
            {
                musicPlayer.IsMute = isMute;
            }
        }

        private void SetSoundVolume(float volume)
        {
            foreach (var soundPlayer in _soundPlayers)
            {
                soundPlayer.Volume = volume;
            }
        }

        private void SetMusicVolume(float volume)
        {
            foreach (var musicPlayer in _musicPlayers)
            {
                musicPlayer.Volume = volume;
            }
        }
        
        public void AddSound(IAudioPlayer soundPlayer)
        {
            soundPlayer.IsMute = _isMuteSound;
            soundPlayer.Volume = _soundVolume;
            _soundPlayers.Add(soundPlayer);
        }

        public void RemoveSound(IAudioPlayer soundPlayer)
        {
            _soundPlayers.Remove(soundPlayer);
        }

        public void AddMusic(IAudioPlayer musicPlayer)
        {
            musicPlayer.IsMute = _isMuteMusic;
            musicPlayer.Volume = _musicVolume;
            _musicPlayers.Add(musicPlayer);
        }

        public void RemoveMusic(IAudioPlayer musicPlayer)
        {
            _musicPlayers.Remove(musicPlayer);
        }
    }
}