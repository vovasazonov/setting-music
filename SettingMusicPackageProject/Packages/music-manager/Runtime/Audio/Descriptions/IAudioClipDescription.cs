using UnityEngine;

namespace Audio
{
    public interface IAudioClipDescription
    {
        string Id { get; }
        AudioClip AudioClip { get; }
    }
}