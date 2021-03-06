﻿using System.Collections.Generic;

namespace Audio
{
    public interface IAudioManager
    {
        IReadOnlyDictionary<string, IAudioCollection> AudioCollections { get; }

        IAudioStopper Play(string audioId, string collectionId);
        IAudioStopper Play(string audioId, string collectionId, IPlaySetting playSetting);
    }
}