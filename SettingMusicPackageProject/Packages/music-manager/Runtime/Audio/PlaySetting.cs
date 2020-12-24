namespace Audio
{
    public struct PlaySetting
    {
        private AudioPriorityType? _audioPriorityType;
        private IPosition _position;
        
        public AudioPriorityType AudioPriorityType
        {
            get => _audioPriorityType ?? AudioPriorityType.Important;
            set => _audioPriorityType = value;
        }

        public IPosition Position
        {
            get => _position ?? new Position(0,0,0);
            set => _position = value;
        }
        
        public float FadeSeconds { get; set; }

        public object ObjectToAttach { get; set; }
    }
}