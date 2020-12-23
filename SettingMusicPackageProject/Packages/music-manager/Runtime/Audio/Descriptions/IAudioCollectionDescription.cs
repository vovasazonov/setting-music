using System.Collections.Generic;

namespace Audio
{
    public interface IAudioCollectionDescription
    {
        string Id { get; }
        IReadOnlyDictionary<AudioPriorityType,int> LimitPlayTogether { get; }
        IEnumerable<IAudioPlayerDescription> AudioPlayerDescriptions { get; }
    }
}