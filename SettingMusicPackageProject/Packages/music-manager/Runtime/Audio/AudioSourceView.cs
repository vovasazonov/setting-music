﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSourceView : MonoBehaviour, IAudioSource
    {
        [SerializeField] private protected AudioSource _audioSource;

        private IReadOnlyDictionary<string, AudioClip> _audioClips;
        private RolloffMode _rolloffMode;

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

        public RolloffMode RolloffMode
        {
            get => _rolloffMode;
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

        public bool IsPlaying => _audioSource.isPlaying;

        public float Time => _audioSource.time;

        internal void Init(IReadOnlyDictionary<string, AudioClip> audioClips)
        {
            _audioClips = audioClips;
        }

        public void Play()
        {
            _audioSource.Play();
        }
        
        public void Stop()
        {
            _audioSource.Stop();
        }

        public void SetPosition(IPosition position)
        {
            _audioSource.transform.position = new Vector3(position.X, position.Y, position.Z);
        }

        public void Attach(object audioAttachableObject)
        {
            _audioSource.transform.SetParent(((MonoBehaviour) audioAttachableObject).transform);
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