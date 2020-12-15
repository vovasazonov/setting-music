using System.Collections.Generic;

namespace Audio
{
    public interface IAudioManager
    {
        IReadOnlyDictionary<string, IAudioCollection> AudioCollections { get; }
        bool IsMuteSound { get; set; }
        bool IsMuteMusic { get; set; }
        float SoundVolume { get; set; }
        float MusicVolume { get; set; }
    }
}