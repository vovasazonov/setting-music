namespace Audio
{
    public interface IPlaySetting
    {
        AudioPriorityType AudioPriorityType { get; }
        float FadeSeconds { get; }
        IPosition Position { get; }
        ITransform FollowTransform { get; }
        bool CanFade { get; }
        bool HasPosition { get; }
        bool CanFollowTransform { get; }
    }
}