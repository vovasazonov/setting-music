namespace Audio
{
    public readonly struct PlaySetting
    {
        private readonly AudioPriorityType? _audioPriorityType;
        private readonly IPosition _position;
        
        public AudioPriorityType AudioPriorityType => _audioPriorityType ?? AudioPriorityType.Important;
        public IPosition Position => _position ?? new Position();
        public float? FadeSeconds { get; }
        public object ObjectToAttach { get; }

        public PlaySetting(AudioPriorityType audioPriorityType = AudioPriorityType.Important, IPosition position = null, float? fadeSeconds = null, object objectToAttach = null)
        {
            _audioPriorityType = audioPriorityType;
            _position = position;
            FadeSeconds = fadeSeconds;
            ObjectToAttach = objectToAttach;
        }
    }
}