using System;

namespace Audio
{
    public delegate void StartAudioPlayHandler(IAudioPlayer audioPlayer);
    public delegate void StopAudioPlayHandler(IAudioPlayer audioPlayer);
    
    public interface IAudioPlayer : IDisposable
    {
        event StartAudioPlayHandler StartPlay;
        event StopAudioPlayHandler StopPlay;
        
        bool IsLoop { get; set; }
        bool IsMute { get; set; }
        bool IsSmooth { get; set; }
        float Volume { get; set; }
        float Pitch { get; set; }
        AudioPriorityType PriorityType { get; set; }
        float SpatialBlend { get; set; }
        float StereoPan { get; set; }
        IAudio3DSetting Audio3DSetting { get; }

        void SetBeginning();
        void Play();
        void Pause();
        void SetPosition(IPosition position);
        void Attach(IAudioAttachable audioAttachableObject);
    }
}