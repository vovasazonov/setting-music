using System.Collections.Generic;

namespace Audio
{
    public interface IAudioDatabase
    {
        IReadOnlyDictionary<AudioPriorityType,int> LimitPlayTogether { get; }
        IEnumerable<IAudioCollectionDescription> AudioCollectionDescription { get; }
    }
}