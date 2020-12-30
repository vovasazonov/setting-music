using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public sealed class AudioSourceView : MonoBehaviour, IAudioSource
    {
        public event StoppedHandler Stopped;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Transform _transform;

        private IReadOnlyDictionary<string, AudioClip> _audioClips;
        private RolloffMode _rolloffMode;
        private IAudioFade _audioFade;
        private ITransform _followTransform;
        private float _lastTime;
        private bool _isBeginPlay;
        private bool _isAlreadyFadeOut;

        public bool IsLoop
        {
            set => _audioSource.loop = value;
        }

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

        internal void Init(IReadOnlyDictionary<string, AudioClip> audioClips)
        {
            _audioClips = audioClips;
            _audioFade = new AudioFade(new AudioSourceVolume(_audioSource));
        }

        private void Update()
        {
            if (_audioSource.isPlaying)
            {
                CheckIsBeginPlay();
                CheckStartFade();
                FollowObject();
                _audioFade.Update(Time.deltaTime);
            }
            else
            {
                CallStopped();
            }
        }

        private void CheckStartFade()
        {
            CheckStartFadeIn();
            CheckStartFadeOut();
        }

        private void CheckIsBeginPlay()
        {
            var time = _audioSource.time;

            if (_lastTime == 0 && time == 0)
            {
                _isBeginPlay = true;
            }
            else
            {
                _isBeginPlay = !(time >= _lastTime);
            }

            _lastTime = time;
        }

        private void CheckStartFadeIn()
        {
            if (_isBeginPlay)
            {
                if (_audioFade.IsFading)
                {
                    _audioFade.StopFade();
                }

                _audioFade.StartFadeIn();
            }
        }

        private void CheckStartFadeOut()
        {
            var timeAudioToEnd = _audioSource.clip.length - _audioSource.time;
            var isTimeSoundLessFadeSeconds = timeAudioToEnd < _audioFade.FadeSeconds;

            if (isTimeSoundLessFadeSeconds && !_isAlreadyFadeOut)
            {
                _isAlreadyFadeOut = true;

                if (_audioFade.IsFading)
                {
                    _audioFade.StopFade();
                }

                _audioFade.StartFadeOut();
            }
            else if (!isTimeSoundLessFadeSeconds && _isAlreadyFadeOut)
            {
                _isAlreadyFadeOut = false;
            }
        }

        public void Play()
        {
            _lastTime = _audioSource.clip.length;
            _isAlreadyFadeOut = false;

            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
            CallStopped();
        }

        private void FollowObject()
        {
            if (!_followTransform.Equals(null))
            {
                var position = _followTransform.Position;
                _transform.position = new Vector3(position.X, position.Y, position.Z);
            }
        }

        public void SetPosition(IPosition position)
        {
            _audioSource.transform.position = new Vector3(position.X, position.Y, position.Z);
        }

        public void Attach(ITransform followTransform)
        {
            _followTransform = followTransform;
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