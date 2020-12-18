using System;

namespace Audio
{
    public delegate void DisposeAudioHandler(IAudioPlayer audioPlayer);

    public interface IAudioPlayer : IDisposable, IIdentifiable<string>
    {
        event CheckAllowPlayHandler CheckAllowPlay;
        event StartPlayAudioHandler StartPlay;
        event FinishPlayAudioHandler FinishPlay;
        event DisposeAudioHandler DisposeAudio;

        bool IsLoop { get; set; }
        bool IsMute { get; set; }
        float FadeInSeconds { get; set; }
        float FadeOutSeconds { get; set; }
        float Volume { get; set; }
        float Pitch { get; set; }
        AudioPriorityType PriorityType { get; set; }
        float SpatialBlend { get; set; }
        float StereoPan { get; set; }
        float Spread { get; set; }
        float DopplerLevel { get; set; }
        float MinDistance { get; set; }
        float MaxDistance { get; set; }

        void Play();
        void Pause();
        void Stop();
        void SetPosition(IPosition position);
        void Attach(object audioAttachableObject);
        void SetVolumeRolloff(RolloffMode rolloffMode);
    }
}