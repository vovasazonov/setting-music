using System.Collections.Generic;

namespace Audio
{
    public interface IAudioManager
    {
        IReadOnlyDictionary<string, IAudioCollection> AudioCollections { get; }

        bool TryGetAudioPlayer(string idAudio, AudioPriorityType audioPriorityType, out IAudioPlayer audioPlayer);
    }
}