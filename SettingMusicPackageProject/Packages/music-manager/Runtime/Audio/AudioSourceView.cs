using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public sealed class AudioSourceView : MonoBehaviour, IAudioSource
    {
        public event StoppedHandler Stopped;

        [SerializeField] private AudioSource _audioSource;

        private IReadOnlyDictionary<string, AudioClip> _audioClips;
        private RolloffMode _rolloffMode;
        private IAudioFade _audioFade;
        private bool _isTimeSoundLessFadeSeconds;

        public bool IsLoop { private get; set; }

        public bool IsMute
        {
            set => _audioSource.mute = value;
        }

        public float Volume
        {
            set
            {
                if (_audioFade.IsFading)
                {
                    _audioFade.UpdateVolume(value);
                }
                else
                {
                    _audioSource.volume = value;
                }
            }
        }

        public float Pitch
        {
            set => _audioSource.pitch = value;
        }

        public float SpatialBlend
        {
            set => _audioSource.spatialBlend = value;
        }

        public float StereoPan
        {
            set => _audioSource.panStereo = value;
        }

        public float Spread
        {
            set => _audioSource.spread = value;
        }

        public float DopplerLevel
        {
            set => _audioSource.dopplerLevel = value;
        }

        public float MinDistance
        {
            set => _audioSource.minDistance = value;
        }

        public float MaxDistance
        {
            set => _audioSource.maxDistance = value;
        }

        public RolloffMode RolloffMode
        {
            set
            {
                _rolloffMode = value;
                switch (value)
                {
                    case RolloffMode.Logarithmic:
                        _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
                        break;
                    case RolloffMode.Linear:
                        _audioSource.rolloffMode = AudioRolloffMode.Linear;
                        break;
                    case RolloffMode.Custom:
                        _audioSource.rolloffMode = AudioRolloffMode.Custom;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_rolloffMode), _rolloffMode, null);
                }
            }
        }

        public float FadeSeconds
        {
            set => _audioFade.FadeSeconds = value;
        }

        private void Update()
        {
            if (_audioSource.isPlaying)
            {
                CheckStartFadeOut();

                _audioFade.Update(Time.deltaTime);
            }
            else if (IsLoop)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }

        private void CheckStartFadeOut()
        {
            if (!_isTimeSoundLessFadeSeconds)
            {
                var timeAudioToEnd = _audioSource.clip.length - _audioSource.time;
                _isTimeSoundLessFadeSeconds = timeAudioToEnd < _audioFade.FadeSeconds;

                if (_isTimeSoundLessFadeSeconds)
                {
                    if (_audioFade.IsFading)
                    {
                        _audioFade.StopFade();
                    }
                    
                    _audioFade.StartFadeOut();
                }
            }
        }

        internal void Init(IReadOnlyDictionary<string, AudioClip> audioClips)
        {
            _audioClips = audioClips;
            _audioFade = new AudioFade(new AudioSourceVolume(_audioSource));
        }

        public void Play()
        {
            _isTimeSoundLessFadeSeconds = false;
            _audioSource.Play();
            if (_audioFade.IsFading)
            {
                _audioFade.StopFade();
            }
            _audioFade.StartFadeIn();
        }

        public void Stop()
        {
            _audioSource.Stop();
            CallStopped();
        }

        public void SetPosition(IPosition position)
        {
            _audioSource.transform.position = new Vector3(position.X, position.Y, position.Z);
        }

        public void Attach(object audioAttachableObject)
        {
            _audioSource.transform.SetParent(((Transform) audioAttachableObject).transform);
        }

        public void SetEnable(bool isEnable)
        {
            _audioSource.gameObject.SetActive(isEnable);
        }

        public void SetClip(string idClip)
        {
            _audioSource.clip = _audioClips[idClip];
        }

        private void CallStopped()
        {
            Stopped?.Invoke();
        }
    }
}