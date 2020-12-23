namespace Audio
{
    public interface IAudioPlayerDescription
    {
        string Id { get; }
        int LimitPlayTogether { get; }
        bool IsLoop { get; }
        float FadeInSeconds { get; }
        float FadeOutSeconds { get; }
        float Pitch { get;  }
        float SpatialBlend { get;  }
        float StereoPan { get; }
        float Spread { get; }
        float DopplerLevel { get;  }
        float MinDistance { get; }
        float MaxDistance { get;  }
        RolloffMode RolloffMode { get; }
    }
}