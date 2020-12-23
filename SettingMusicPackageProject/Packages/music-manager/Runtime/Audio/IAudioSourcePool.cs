namespace Audio
{
    public interface IAudioSourcePool
    {
        IAudioSource Take();
        void Return(IAudioSource audioSource);
    }
}