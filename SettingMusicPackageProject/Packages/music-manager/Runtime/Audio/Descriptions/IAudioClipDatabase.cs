using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public interface IAudioClipDatabase
    {
        IReadOnlyDictionary<string, AudioClip> AudioClips { get; }
    }
}