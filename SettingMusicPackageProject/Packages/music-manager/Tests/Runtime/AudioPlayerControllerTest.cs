using Audio;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class AudioPlayerControllerTest
    {
        [Test]
        [TestCase(2,1)]
        public void IsAmountPlayingLessLimit_AudioPlayersPlayingNotMoreLimit_ReturnTrue(int limit, int amountPlaying)
        {
            var audioPlayerDescription = Substitute.For<IAudioPlayerDescription>();
            audioPlayerDescription.LimitPlayTogether.Returns(limit);
            var audioSourcePool = Substitute.For<IAudioSourcePool>();
            AudioPlayerController audioPlayerController = new AudioPlayerController(audioPlayerDescription, audioSourcePool);

            for (int i = 0; i < amountPlaying; i++)
            {
                audioPlayerController.GetAudioPlayer();
            }
            
            Assert.IsTrue(audioPlayerController.IsAmountPlayingLessLimit());
        }
        
        [Test]
        [TestCase(2,3)]
        public void IsAmountPlayingLessLimit_AudioPlayersPlayingMoreLimit_ReturnTrue(int limit, int amountPlaying)
        {
            var audioPlayerDescription = Substitute.For<IAudioPlayerDescription>();
            audioPlayerDescription.LimitPlayTogether.Returns(limit);
            var audioSourcePool = Substitute.For<IAudioSourcePool>();
            AudioPlayerController audioPlayerController = new AudioPlayerController(audioPlayerDescription, audioSourcePool);

            for (int i = 0; i < amountPlaying; i++)
            {
                audioPlayerController.GetAudioPlayer();
            }
            
            Assert.IsTrue(audioPlayerController.IsAmountPlayingLessLimit());
        }
        
        [Test]
        [TestCase(2,2)]
        public void IsAmountPlayingLessLimit_AudioPlayersPlayingQuealsLimit_ReturnFalse(int limit, int amountPlaying)
        {
            var audioPlayerDescription = Substitute.For<IAudioPlayerDescription>();
            audioPlayerDescription.LimitPlayTogether.Returns(limit);
            var audioSourcePool = Substitute.For<IAudioSourcePool>();
            AudioPlayerController audioPlayerController = new AudioPlayerController(audioPlayerDescription, audioSourcePool);

            for (int i = 0; i < amountPlaying; i++)
            {
                audioPlayerController.GetAudioPlayer();
            }
            
            Assert.IsTrue(audioPlayerController.IsAmountPlayingLessLimit());
        }

        [Test]
        public void GetAudioPlayer_NotNull_True()
        {
            var audioPlayerDescription = Substitute.For<IAudioPlayerDescription>();
            var audioSourcePool = Substitute.For<IAudioSourcePool>();
            AudioPlayerController audioPlayerController = new AudioPlayerController(audioPlayerDescription, audioSourcePool);

            var audioPlayer = audioPlayerController.GetAudioPlayer();
            
            Assert.IsNotNull(audioPlayer);
        }
    }
}