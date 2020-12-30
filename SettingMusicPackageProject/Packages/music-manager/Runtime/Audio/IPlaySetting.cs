namespace Audio
{
    public interface IPlaySetting
    {
        AudioPriorityType AudioPriorityType { get; }
        IPosition Position { get; }
        float? FadeSeconds { get; }
        ITransform FollowTransform { get; }
    }
}