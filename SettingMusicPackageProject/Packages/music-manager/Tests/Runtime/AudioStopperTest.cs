using System;
using NUnit.Framework;
using Audio;

namespace Tests
{
    [TestFixture]
    public class AudioStopperTest
    {
        [Test]
        public void Stop_AudioPlayerIsNull_ThrowNullReferenceException()
        {
            var audioStopper = new AudioStopper(null);

            Assert.Throws<NullReferenceException>(()=>audioStopper.Stop());
        }
    }
}