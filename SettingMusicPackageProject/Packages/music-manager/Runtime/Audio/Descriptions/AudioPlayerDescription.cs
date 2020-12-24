using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioPlayerDescription", menuName = "AudioPackage/AudioPlayerDescription", order = 0)]
    public sealed class AudioPlayerDescription : ScriptableObject, IAudioPlayerDescription
    {
        [SerializeField] private string _audioId;
        [SerializeField] private string _clipId;
        [SerializeField] private float _volume = 1;
        [SerializeField] private int _limitPlayTogether = 2;
        [SerializeField] private bool _isLoop;
        [SerializeField] private float _fadeSeconds;
        [SerializeField] private float _pitch = 1;
        [SerializeField] private float _spatialBlend;
        [SerializeField] private float _stereoPan;
        [SerializeField] private float _spread;
        [SerializeField] private float _dopplerLevel = 1;
        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        [SerializeField] private RolloffMode _rolloffMode = RolloffMode.Logarithmic;

        public string Id => _audioId;
        public string ClipId => _clipId;
        public float Volume => _volume;
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