using System.Collections.Generic;

namespace Audio
{
    public interface IAudioManager
    {
        IReadOnlyDictionary<string, IAudioCollection> MusicCollections { get; }
        IReadOnlyDictionary<string, IAudioCollection> SoundCollections { get; }
        bool IsMuteMusic { get; set; }
        bool IsMuteSound { get; set; }
        float MusicVolume { get; set; }
        float SoundVolume { get; set; }
    }
}