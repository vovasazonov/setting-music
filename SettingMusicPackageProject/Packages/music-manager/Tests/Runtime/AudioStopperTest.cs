using NUnit.Framework;
using Audio;

namespace Tests
{
    [TestFixture]
    public class AudioStopperTest
    {
        [Test]
        public void Stop_AudioPlayerIsNull_NotThrowException()
        {
            var audioStopper = new AudioStopper(null);

            Assert.DoesNotThrow(()=>audioStopper.Stop());
        }
    }
}