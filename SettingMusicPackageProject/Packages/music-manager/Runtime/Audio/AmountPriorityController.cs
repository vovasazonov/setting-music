using System;
using System.Collections.Generic;

namespace Audio
{
    public sealed class AmountPriorityController : IAmountPriorityController
    {
        private readonly IDictionary<AudioPriorityType, int> _priorityByAudioPlayerAmountPlaying = new Dictionary<AudioPriorityType, int>();
        private readonly IDictionary<IAudioPlayer, AudioPriorityType> _audioPlayerPlayingByPriority = new Dictionary<IAudioPlayer, AudioPriorityType>();
        private readonly IDictionary<AudioPriorityType, int> _priorityByLimitPlayTogether = new Dictionary<AudioPriorityType, int>();

        public AmountPriorityController(IReadOnlyDictionary<AudioPriorityType, int> limitPlayTogether)
        {
            foreach (AudioPriorityType priority in Enum.GetValues(typeof(AudioPriorityType)))
            {
                _priorityByAudioPlayerAmountPlaying[priority] = 0;
                _priorityByLimitPlayTogether[priority] = 0;
            }

            foreach (var priority in limitPlayTogether.Keys)
            {
                _priorityByLimitPlayTogether[priority] = limitPlayTogether[priority];
            }
        }

        public bool CheckSpaceAvailable(AudioPriorityType audioPriorityType)
        {
            return _priorityByLimitPlayTogether[audioPriorityType] > _priorityByAudioPlayerAmountPlaying[audioPriorityType];
        }

        public void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer)
        {
            _priorityByAudioPlayerAmountPlaying[audioPriorityType] += 1;
            _audioPlayerPlayingByPriority[audioPlayer] = audioPriorityType;
        }

        public void RemoveAudioPlayer(IAudioPlayer audioPlayer)
        {
            var priorityType = _audioPlayerPlayingByPriority[audioPlayer];
            _audioPlayerPlayingByPriority.Remove(audioPlayer);
            _priorityByAudioPlayerAmountPlaying[priorityType] -= 1;
        }
    }
}