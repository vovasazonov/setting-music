using System.Collections.Generic;

namespace Audio
{
    public interface IAudioManager
    {
        IReadOnlyDictionary<string, IAudioCollection> AudioCollections { get; }

        IAudioPlayer Play(string audioId, string collectionId, PlaySetting playSetting = new PlaySetting());
    }
}