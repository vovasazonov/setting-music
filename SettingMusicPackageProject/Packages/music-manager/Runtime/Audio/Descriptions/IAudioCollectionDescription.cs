using System.Collections.Generic;

namespace Audio
{
    public interface IAudioCollectionDescription
    {
        string Id { get; }
        IReadOnlyDictionary<AudioPriorityType,int> LimitAudioPriority { get; }
        IReadOnlyDictionary<string, IAudioPlayerDescription> AudioPlayerDescriptions { get; }
    }
}