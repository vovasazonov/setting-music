using NUnit.Framework;
using Audio;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class AudioFadeTest
    {
        [Test]
        public void StartFadeIn_IsFading_True()
        {
            var volume = Substitute.For<IVolume>();
            var audioFade = new AudioFade(volume);
            
            audioFade.StartFadeIn();
            
            Assert.IsTrue(audioFade.IsFading);
        }
        
        [Test]
        public void StartFadeOut_IsFading_True()
        {
            var volume = Substitute.For<IVolume>();
            var audioFade = new AudioFade(volume);
            
            audioFade.StartFadeOut();
            
            Assert.IsTrue(audioFade.IsFading);
        }
        
        [Test]
        public void StopFade_IsFading_True()
        {
            var volume = Substitute.For<IVolume>();
            var audioFade = new AudioFade(volume);
            
            audioFade.StartFadeIn();
            audioFade.StopFade();

            Assert.IsFalse(audioFade.IsFading);
        }
    }
}