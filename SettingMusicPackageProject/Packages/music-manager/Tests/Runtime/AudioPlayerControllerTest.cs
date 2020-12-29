using System.Collections.Generic;
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
        public void IsAmountPlayingLessLimit_AmountPlayingLessLimit_ReturnTrue(int limit, int amountPlaying)
        {
            var audioPlayerController = CreateAudioPlayerController(limit);

            var audioPlayers = GetAudioPlayers(amountPlaying, audioPlayerController);

            Assert.IsTrue(audioPlayerController.IsAmountPlayingLessLimit());
        }

        [Test]
        [TestCase(2,3)]
        public void IsAmountPlayingLessLimit_AmountPlayingMoreLimit_ReturnFalse(int limit, int amountPlaying)
        {
            AudioPlayerController audioPlayerController = CreateAudioPlayerController(limit);

            var audioPlayers = GetAudioPlayers(amountPlaying, audioPlayerController);

            Assert.IsFalse(audioPlayerController.IsAmountPlayingLessLimit());
        }
        
        [Test]
        [TestCase(2,2)]
        public void IsAmountPlayingLessLimit_AmountPlayingEqualsLimit_ReturnFalse(int limit, int amountPlaying)
        {
            AudioPlayerController audioPlayerController = CreateAudioPlayerController(limit);

            var audioPlayers = GetAudioPlayers(amountPlaying, audioPlayerController);

            Assert.IsFalse(audioPlayerController.IsAmountPlayingLessLimit());
        }
        
        [Test]
        [TestCase(2,5, 4)]
        public void IsAmountPlayingLessLimit_AudioPlayersDisposedToGetLessLimit_ReturnTrue(int limit, int amountPlaying, int amountDispose)
        {
            AudioPlayerController audioPlayerController = CreateAudioPlayerController(limit);

            var audioPlayers = GetAudioPlayers(amountPlaying, audioPlayerController);
            DisposeAudioPlayers(amountDispose, audioPlayers);
            
            Assert.IsTrue(audioPlayerController.IsAmountPlayingLessLimit());
        }

        [Test]
        public void GetAudioPlayer_AmountPlayingEqualsOrMoreLimit_ReturnNotNull()
        {
            AudioPlayerController audioPlayerController = CreateAudioPlayerController(0);

            var audioPlayer = audioPlayerController.GetAudioPlayer();
            
            Assert.IsNotNull(audioPlayer);
        }

        private List<IAudioPlayer> GetAudioPlayers(int amount, IAudioPlayerController audioPlayerController)
        {
            var players = new List<IAudioPlayer>();
            
            for (int i = 0; i < amount; i++)
            {
                players.Add(audioPlayerController.GetAudioPlayer());
            }

            return players;
        }
        
        private void DisposeAudioPlayers(int amountDispose, List<IAudioPlayer> audioPlayers)
        {
            for (int i = 0; i < amountDispose; i++)
            {
                audioPlayers[i].Dispose();
            }
        }
        
        private AudioPlayerController CreateAudioPlayerController(int limit)
        {
            var audioPlayerDescription = Substitute.For<IAudioPlayerDescription>();
            audioPlayerDescription.LimitPlayTogether.Returns(limit);
            var audioSourcePool = Substitute.For<IAudioSourcePool>();
            AudioPlayerController audioPlayerController = new AudioPlayerController(audioPlayerDescription, audioSourcePool);
            return audioPlayerController;
        }
    }
}