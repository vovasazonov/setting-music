using System.Collections.Generic;

namespace Audio
{
    public interface IAudioManager
    {
        IReadOnlyDictionary<string, IAudioCollection> AudioCollections { get; }

        IAudioPlayer Play(string idAudio, PlaySetting playSetting = new PlaySetting());
    }
}