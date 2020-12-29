using Audio;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AudioPlayerTest
    {
        [Test]
        public void Stop_AudioPlayerDisposing_AudioDisposingTrue()
        {
            bool isDisposingCalled = false;
            var audioPlayerDescription = Substitute.For<IAudioPlayerDescription>();
            var audioSource = Substitute.For<IAudioSource>();
            audioSource.When(a=>a.Stop()).Do(a=>audioSource.Stopped += Raise.Event<StoppedHandler>());
            var audioPlayer = new AudioPlayer(audioPlayerDescription, audioSource);
            audioPlayer.Disposing += dependency => isDisposingCalled = true;
            
            audioPlayer.Stop();
            
            Assert.IsTrue(isDisposingCalled);
        }
    }
}