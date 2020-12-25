using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSourceView : MonoBehaviour, IAudioSource
    {
        [SerializeField] private protected AudioSource _audioSource;

        private IReadOnlyDictionary<string, AudioClip> _audioClips;
        private RolloffMode _rolloffMode;

        public bool IsLoop { private get; set; }

        public bool IsMute
        {
            set => _audioSource.mute = value;
        }

        public float Volume
        {
            private get => _audioSource.volume;
            set => _audioSource.volume = value;
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
            StartCoroutine(FollowAudioPlayToFade());
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
            StopAllCoroutines();
        }

        private IEnumerator Fade(float startVolume, float targetVolume, float fadeSeconds)
        {
            var originalVolume = Volume;

            if (fadeSeconds > 0)
            {
                float currentSeconds = 0;

                while (currentSeconds < fadeSeconds)
                {
                    currentSeconds += Time.deltaTime;
                    Volume = Mathf.Lerp(startVolume, targetVolume, currentSeconds / fadeSeconds);

                    yield return null;
                }

                Volume = originalVolume;
            }
        }

        private IEnumerator FollowAudioPlayToFade()
        {
            bool isFadeIn = false;
            bool isFadeOut = false;

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
                }

                yield return null;
            }
        }

        private bool FadeIn()
        {
            StartCoroutine(Fade(0, Volume, FadeSeconds));
            return true;
        }

        private bool FadeOut()
        {
            bool isFadeOut;
            var timeAudioToEnd = _audioSource.clip.length - _audioSource.time;
            if (timeAudioToEnd < FadeSeconds)
            {
                StartCoroutine(Fade(Volume, 0, FadeSeconds));
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
    }
}