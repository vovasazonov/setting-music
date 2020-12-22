using System;
using System.Collections.Generic;

namespace Audio
{
    public sealed class AmountPriorityAudioPlayerController
    {
        private readonly IDictionary<AudioPriorityType, int> _priorityToAudioPlayerAmountPlaying = new Dictionary<AudioPriorityType, int>();
        private readonly IDictionary<IAudioPlayer, AudioPriorityType> _audioPlayerPlayingToPriority = new Dictionary<IAudioPlayer, AudioPriorityType>();
        private readonly IDictionary<AudioPriorityType, int> _limitPlayTogether = new Dictionary<AudioPriorityType, int>();

        public AmountPriorityAudioPlayerController(IReadOnlyDictionary<AudioPriorityType, int> limitPlayTogether)
        {
            foreach (AudioPriorityType priority in Enum.GetValues(typeof(AudioPriorityType)))
            {
                _priorityToAudioPlayerAmountPlaying[priority] = 0;
                _limitPlayTogether[priority] = 0;
            }

            foreach (var priority in limitPlayTogether.Keys)
            {
                _limitPlayTogether[priority] = limitPlayTogether[priority];
            }
        }

        public bool FreeSpaceAvailable(AudioPriorityType audioPriorityType)
        {
            return _limitPlayTogether[audioPriorityType] > _priorityToAudioPlayerAmountPlaying[audioPriorityType];
        }

        public void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer)
        {
            _priorityToAudioPlayerAmountPlaying[audioPriorityType] += 1;
            _audioPlayerPlayingToPriority[audioPlayer] = audioPriorityType;
        }

        public void RemoveAudioPlayer(IAudioPlayer audioPlayer)
        {
            var priorityType = _audioPlayerPlayingToPriority[audioPlayer];
            _audioPlayerPlayingToPriority.Remove(audioPlayer);
            _priorityToAudioPlayerAmountPlaying[priorityType] -= 1;
        }
    }
}