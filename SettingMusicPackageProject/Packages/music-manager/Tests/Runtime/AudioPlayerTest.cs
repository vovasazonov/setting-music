using Audio;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AudioPlayerTest
    {
        [Test]
        public void Stop_StopAudio_DisposeCalled()
        {
            var audioPlayerDescription = Substitute.For<IAudioPlayerDescription>();
            var audioSource = Substitute.For<IAudioSource>();
            var audioPlayer = new AudioPlayer(audioPlayerDescription, audioSource);
            bool isDisposingCalled = false;
            audioSource.When(a=>a.Stop()).Do(a=>audioSource.Stopped += Raise.Event<StoppedHandler>());
            audioPlayer.Disposing += dependency => isDisposingCalled = true;
            
            audioPlayer.Stop();
            
            Assert.IsTrue(isDisposingCalled);
        }
    }
}