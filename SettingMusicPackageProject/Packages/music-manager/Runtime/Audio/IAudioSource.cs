namespace Audio
{
    public delegate void StoppedHandler();

    public interface IAudioSource
    {
        event StoppedHandler Stopped;
        
        bool IsLoop { set; }
        bool IsMute { set; }
        float Volume { set; }
        float Pitch { set; }
        float SpatialBlend { set; }
        float StereoPan { set; }
        float Spread { set; }
        float DopplerLevel { set; }
        float MinDistance { set; }
        float MaxDistance { set; }
        RolloffMode RolloffMode { set; } 
        float FadeSeconds { set; }

        void Play();
        void Stop();
        void SetPosition(IPosition position);
        void Attach(object audioAttachableObject);
        void SetEnable(bool isEnable);
        void SetClip(string idClip);
    }
}