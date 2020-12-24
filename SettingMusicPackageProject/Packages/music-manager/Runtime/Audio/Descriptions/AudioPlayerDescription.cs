using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioPlayerDescription", menuName = "AudioPackage/AudioPlayerDescription", order = 0)]
    public class AudioPlayerDescription : ScriptableObject, IAudioPlayerDescription
    {
        [SerializeField] private protected string _audioId;
        [SerializeField] private protected string _clipId;
        [SerializeField] private protected int _limitPlayTogether = 2;
        [SerializeField] private protected bool _isLoop;
        [SerializeField] private protected float _fadeSeconds;
        [SerializeField] private protected float _pitch = 1;
        [SerializeField] private protected float _spatialBlend;
        [SerializeField] private protected float _stereoPan;
        [SerializeField] private protected float _spread;
        [SerializeField] private protected float _dopplerLevel = 1;
        [SerializeField] private protected float _minDistance;
        [SerializeField] private protected float _maxDistance;
        [SerializeField] private protected RolloffMode _rolloffMode = RolloffMode.Logarithmic;

        public string Id => _audioId;
        public string ClipId => _clipId;
        public int LimitPlayTogether => _limitPlayTogether;
        public bool IsLoop => _isLoop;
        public float FadeSeconds => _fadeSeconds;
        public float Pitch => _pitch;
        public float SpatialBlend => _spatialBlend;
        public float StereoPan => _stereoPan;
        public float Spread => _spread;
        public float DopplerLevel => _dopplerLevel;
        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;
        public RolloffMode RolloffMode => _rolloffMode;
    }
}