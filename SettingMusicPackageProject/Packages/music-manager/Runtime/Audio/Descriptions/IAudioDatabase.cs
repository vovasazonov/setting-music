using System.Collections.Generic;

namespace Audio
{
    public interface IAudioDatabase
    {
        IReadOnlyDictionary<string, IAudioCollectionDescription> AudioCollectionDescriptionDic { get; }
        IReadOnlyDictionary<string, IAudioPlayerDescription> AudioPlayerDescriptionDic { get; }
    }
}