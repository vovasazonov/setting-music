using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        public event CheckAllowPlayHandler CheckAllowPlay;
        public event StartPlayAudioHandler StartPlay;
        public event FinishPlayAudioHandler FinishPlay;
        public event DisposeAudioHandler AudioDispose;
        
        [SerializeField] private protected string _id;
        [SerializeField] private protected float _fadeInSeconds;
        [SerializeField] private protected float _fadeOutSeconds;
        [SerializeField] private protected AudioPriorityType _audioPriorityType;

        private AudioSource _audioSource;
        private bool _isPause;
        private bool _isOnPlayProcess;

        public string Id => _id;

        public bool IsLoop
        {
            get => _audioSource.loop;
            set => _audioSource.loop = value;
        }

        public bool IsMute
        {
            get => _audioSource.mute;
            set => _audioSource.mute = value;
        }

        public float FadeInSeconds
        {
            get => _fadeInSeconds;
            set => _fadeInSeconds = value;
        }

        public float FadeOutSeconds
        {
            get => _fadeOutSeconds;
            set => _fadeOutSeconds = value;
        }

        public float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        public float Pitch
        {
            get => _audioSource.pitch;
            set => _audioSource.pitch = value;
        }

        public AudioPriorityType PriorityType
        {
            get => _audioPriorityType;
            set
            {
                _audioPriorityType = value;
                switch (_audioPriorityType)
                {
                    case AudioPriorityType.High:
                        _audioSource.priority = 0;
                        break;
                    case AudioPriorityType.Medium:
                        _audioSource.priority = 128;
                        break;
                    case AudioPriorityType.Low:
                        _audioSource.priority = 256;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public float SpatialBlend
        {
            get => _audioSource.spatialBlend;
            set => _audioSource.spatialBlend = value;
        }

        public float StereoPan
        {
            get => _audioSource.panStereo;
            set => _audioSource.panStereo = value;
        }

        public float Spread
        {
            get => _audioSource.spread;
            set => _audioSource.spread = value;
        }

        public float DopplerLevel
        {
            get => _audioSource.dopplerLevel;
            set => _audioSource.dopplerLevel = value;
        }

        public float MinDistance
        {
            get => _audioSource.minDistance;
            set => _audioSource.minDistance = value;
        }

        public float MaxDistance
        {
            get => _audioSource.maxDistance;
            set => _audioSource.maxDistance = value;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            bool isAllowPlay = true;
            CallCheckAllowPlay(this, ref isAllowPlay);
            
            _isPause = false;
            
            if (isAllowPlay)
            {
                if (!_isOnPlayProcess)
                {
                    _isOnPlayProcess = true;
                    StartCoroutine(Fade(0, Volume, _fadeInSeconds));
                    StartCoroutine(FollowAudioPlay());
                }
                
                CallStartPlay(this);
                _audioSource.Play();
            }
            else
            {
                Stop();
            }
        }

        public void Pause()
        {
            _isPause = true;
            _audioSource.Pause();
        }

        public void Stop()
        {
            _isOnPlayProcess = false;
            _audioSource.Stop();
            CallFinishPlay(this);
        }

        private IEnumerator FollowAudioPlay()
        {
            bool isStartFade = false;

            while (_audioSource.isPlaying || _isPause)
            {
                if (!isStartFade)
                {
                    var timeAudioToEnd = _audioSource.clip.length - _audioSource.time;
                    if (timeAudioToEnd < _fadeOutSeconds)
                    {
                        StartCoroutine(Fade(Volume, 0, _fadeOutSeconds));
                        isStartFade = true;
                    }
                }

                yield return null;
            }

            Stop();
        }

        private IEnumerator Fade(float startVolume, float targetVolume, float fadeSeconds)
        {
            var originalVolume = Volume;

            if (fadeSeconds > 0)
            {
                float currentSeconds = 0;

                while (currentSeconds < _fadeOutSeconds)
                {
                    if (!_isPause)
                    {
                        currentSeconds += Time.deltaTime;
                        Volume = Mathf.Lerp(startVolume, targetVolume, currentSeconds / fadeSeconds);
                    }

                    yield return null;
                }

                Volume = originalVolume;
            }
        }

        public void SetPosition(IPosition position)
        {
            transform.position = new Vector3(position.X, position.Y, position.Z);
        }

        public void Attach(object audioAttachableObject)
        {
            transform.SetParent(((MonoBehaviour) audioAttachableObject).transform);
        }

        public void SetVolumeRolloff(RolloffMode rolloffMode)
        {
            switch (rolloffMode)
            {
                case RolloffMode.Logarithmic:
                    _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
                    break;
                case RolloffMode.Linear:
                    _audioSource.rolloffMode = AudioRolloffMode.Linear;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rolloffMode), rolloffMode, null);
            }
        }

        public void Dispose()
        {
            CallAudioDispose(this);
        }

        private void CallStartPlay(IAudioPlayer audioPlayer)
        {
            StartPlay?.Invoke(audioPlayer);
        }

        private void CallFinishPlay(IAudioPlayer audioPlayer)
        {
            FinishPlay?.Invoke(audioPlayer);
        }

        private void CallAudioDispose(IAudioPlayer audioPlayer)
        {
            AudioDispose?.Invoke(audioPlayer);
        }

        private void CallCheckAllowPlay(IAudioPlayer audioPlayer, ref bool isAllowPlay)
        {
            CheckAllowPlay?.Invoke(audioPlayer, ref isAllowPlay);
        }
    }
}