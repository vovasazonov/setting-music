using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSourceView : MonoBehaviour, IAudioSource
    {
        public event StoppedHandler Stopped;
        
        [SerializeField] private protected AudioSource _audioSource;

        private IReadOnlyDictionary<string, AudioClip> _audioClips;
        private RolloffMode _rolloffMode;
        private float _volume;
        private bool _isFading;

        public bool IsLoop { private get; set; }

        public bool IsMute
        {
            set => _audioSource.mute = value;
        }

        public float Volume
        {
            set
            {
                _volume = value;
                if (!_isFading)
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

        public float FadeSeconds { private get; set; }

        internal void Init(IReadOnlyDictionary<string, AudioClip> audioClips)
        {
            _audioClips = audioClips;
        }

        public void Play()
        {
            StartCoroutine(FollowAudioPlay());
        }

        public void Stop()
        {
            _audioSource.Stop();
            StopAllCoroutines();
            ResetVariablesChangedByFadingToOrigins();
            CallStopped();
        }

        private IEnumerator Fade(float startVolume, float targetVolume, float fadeSeconds)
        {
            if (fadeSeconds > 0)
            {
                _isFading = true;
                float currentSeconds = 0;

                while (currentSeconds < fadeSeconds)
                {
                    currentSeconds += Time.deltaTime;
                    _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentSeconds / fadeSeconds);

                    yield return null;
                }

                ResetVariablesChangedByFadingToOrigins();
            }
        }

        private void ResetVariablesChangedByFadingToOrigins()
        {
            _isFading = false;
            _audioSource.volume = _volume;
        }

        private IEnumerator FollowAudioPlay()
        {
            bool isFadeIn = false;
            bool isFadeOut = false;
            _audioSource.Play();
            
            while (_audioSource.isPlaying || IsLoop)
            {
                if (!isFadeIn)
                {
                    isFadeIn = FadeIn();
                }

                if (_audioSource.isPlaying)
                {
                    if (!isFadeOut)
                    {
                        isFadeOut = FadeOut();
                    }
                }
                else if (IsLoop)
                {
                    isFadeOut = false;
                    isFadeIn = false;
                    _audioSource.Play();
                }

                yield return null;
            }

            CallStopped();
        }

        private bool FadeIn()
        {
            StartCoroutine(Fade(0, _volume, FadeSeconds));
            return true;
        }

        private bool FadeOut()
        {
            bool isFadeOut = false;
            var timeAudioToEnd = _audioSource.clip.length - _audioSource.time;
            if (timeAudioToEnd < FadeSeconds)
            {
                StartCoroutine(Fade(_volume, 0, FadeSeconds));
                isFadeOut = true;
            }

            return isFadeOut;
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