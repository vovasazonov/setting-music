namespace Audio
{
    public sealed class PlaySetting : IPlaySetting
    {
        public bool CanFade { get; }
        public bool HasPosition { get; }
        public bool CanFollowTransform { get; }

        public AudioPriorityType AudioPriorityType { get; }
        public float FadeSeconds { get; }
        public IPosition Position { get; }
        public ITransform FollowTransform { get; }

        public PlaySetting(AudioPriorityType audioPriorityType, float fadeSeconds, IPosition position, ITransform followTransform)
        {
            AudioPriorityType = audioPriorityType;

            if (position != null)
            {
                HasPosition = true;
                Position = position;
            }

            if (followTransform != null)
            {
                CanFollowTransform = true;
                FollowTransform = followTransform;
            }

            if (fadeSeconds >= 0)
            {
                CanFade = true;
                FadeSeconds = fadeSeconds;
            }
        }

        public static PlaySetting Default()
        {
            return new PlaySetting(AudioPriorityType.Important, -1, null, null);
        }

        public static PlaySetting Custom(AudioPriorityType audioPriorityType = AudioPriorityType.Important, float fadeSeconds = -1, IPosition position = null, ITransform followTransform = null)
        {
            return new PlaySetting(audioPriorityType, fadeSeconds, position, followTransform);
        }
    }
}