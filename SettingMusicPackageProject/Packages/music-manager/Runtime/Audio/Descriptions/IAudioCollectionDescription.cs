using System.Collections.Generic;

namespace Audio
{
    public interface IAudioCollectionDescription
    {
        string Id { get; }
        float Volume { get; }
        IReadOnlyDictionary<AudioPriorityType,int> LimitAudioPriorityDic { get; }
    }
}