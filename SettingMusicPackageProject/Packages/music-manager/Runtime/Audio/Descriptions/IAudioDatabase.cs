using System.Collections.Generic;

namespace Audio
{
    public interface IAudioDatabase
    {
        IReadOnlyDictionary<AudioPriorityType, int> LimitAudioPriorityDic { get; }
        IReadOnlyDictionary<string, IAudioCollectionDescription> AudioCollectionDescriptionDic { get; }
    }
}