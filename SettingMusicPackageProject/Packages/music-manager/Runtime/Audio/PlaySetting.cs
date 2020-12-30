namespace Audio
{
    public readonly struct PlaySetting : IPlaySetting
    {
        private readonly AudioPriorityType? _audioPriorityType;

        public AudioPriorityType AudioPriorityType => _audioPriorityType ?? AudioPriorityType.Important;
        public IPosition Position { get; }
        public float? FadeSeconds { get; }
        public ITransform FollowTransform { get; }

        public PlaySetting(AudioPriorityType audioPriorityType = AudioPriorityType.Important, IPosition position = null, float? fadeSeconds = null, ITransform followTransform = null)
        {
            _audioPriorityType = audioPriorityType;
            Position = position;
            FadeSeconds = fadeSeconds;
            FollowTransform = followTransform;
        }
    }
}