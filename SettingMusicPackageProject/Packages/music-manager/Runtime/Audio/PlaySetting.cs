namespace Audio
{
    public readonly struct PlaySetting
    {
        private readonly AudioPriorityType? _audioPriorityType;

        public AudioPriorityType AudioPriorityType => _audioPriorityType ?? AudioPriorityType.Important;
        public IPosition Position { get; }
        public float? FadeSeconds { get; }
        public object ObjectToAttach { get; }

        public PlaySetting(AudioPriorityType audioPriorityType = AudioPriorityType.Important, IPosition position = null, float? fadeSeconds = null, object objectToAttach = null)
        {
            _audioPriorityType = audioPriorityType;
            Position = position;
            FadeSeconds = fadeSeconds;
            ObjectToAttach = objectToAttach;
        }
    }
}