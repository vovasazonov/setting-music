using System.Collections.Generic;
using NUnit.Framework;
using Audio;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class AmountPriorityControllerTest
    {
        [Test]
        [TestCase(AudioPriorityType.Important, 1, 0)]
        [TestCase(AudioPriorityType.Important, 2, 1)]
        [TestCase(AudioPriorityType.Least, 2, 1)]
        [TestCase(AudioPriorityType.Unimportant, 2, 1)]
        public void CheckSpaceAvailable_PlayingLessLimit_SpaceAvailable(AudioPriorityType audioPriorityType, int limit, int amountPlayers)
        {
            var amountPriorityController = CreateAmountPriorityController(audioPriorityType, limit);

            var audioPlayers = AddAudioPlayers(amountPlayers, audioPriorityType, amountPriorityController);
            
            Assert.IsTrue(amountPriorityController.CheckSpaceAvailable(audioPriorityType));
        }
        
        [TestCase(AudioPriorityType.Important, 0, 0)]
        [TestCase(AudioPriorityType.Important, 2, 2)]
        [TestCase(AudioPriorityType.Least, 2, 2)]
        [TestCase(AudioPriorityType.Unimportant, 5, 9)]
        public void CheckSpaceAvailable_PlayingMoreLimit_SpaceNotAvailable(AudioPriorityType audioPriorityType, int limit, int amountPlayers)
        {
            var amountPriorityController = CreateAmountPriorityController(audioPriorityType, limit);

            var audioPlayers = AddAudioPlayers(amountPlayers, audioPriorityType, amountPriorityController);
            
            Assert.IsFalse(amountPriorityController.CheckSpaceAvailable(audioPriorityType));
        }

        private AmountPriorityController CreateAmountPriorityController(AudioPriorityType audioPriorityType, int limit)
        {
            return new AmountPriorityController(new Dictionary<AudioPriorityType, int> {{audioPriorityType, limit}});
        }

        private List<IAudioPlayer> AddAudioPlayers(int amountAdd, AudioPriorityType audioPriorityType, AmountPriorityController amountPriorityController)
        {
            var players = new List<IAudioPlayer>();

            for (int i = 0; i < amountAdd; i++)
            {
                var audioPlayer = Substitute.For<IAudioPlayer>();
                players.Add(audioPlayer);
                amountPriorityController.AddAudioPlayer(audioPriorityType, audioPlayer);
            }

            return players;
        }
    }
}