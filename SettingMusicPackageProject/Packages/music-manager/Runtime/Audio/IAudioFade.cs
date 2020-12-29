namespace Audio
{
    internal interface IAudioFade
    {
        bool IsFading { get; }
        float FadeSeconds { get; set; }

        void UpdateVolume(float volume);
        void StartFadeIn();
        void StartFadeOut();
        void StopFade();
        void Update(float deltaTime);
    }
}