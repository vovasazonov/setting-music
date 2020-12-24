using System.Collections.Generic;

namespace Audio
{
    public interface IAudioDatabase
    {
        IReadOnlyDictionary<AudioPriorityType, int> LimitPriorityPlayTogether { get; }
        IReadOnlyDictionary<string, IAudioCollectionDescription> AudioCollectionDescriptions { get; }
    }
}