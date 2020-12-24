using System;
using System.Collections.Generic;

namespace Audio
{
    public sealed class AmountPriorityController : IAmountPriorityController
    {
        private readonly IDictionary<AudioPriorityType, int> _priorityByAmountPlaying = new Dictionary<AudioPriorityType, int>();
        private readonly IDictionary<IAudioPlayer, AudioPriorityType> _audioPlayingByAudioPriority = new Dictionary<IAudioPlayer, AudioPriorityType>();
        private readonly IDictionary<AudioPriorityType, int> _priorityByLimitPlay = new Dictionary<AudioPriorityType, int>();

        public AmountPriorityController(IReadOnlyDictionary<AudioPriorityType, int> priorityByLimitPlay)
        {
            foreach (AudioPriorityType priority in Enum.GetValues(typeof(AudioPriorityType)))
            {
                _priorityByAmountPlaying[priority] = 0;
                _priorityByLimitPlay[priority] = 0;
            }

            foreach (var priority in priorityByLimitPlay.Keys)
            {
                _priorityByLimitPlay[priority] = priorityByLimitPlay[priority];
            }
        }

        public bool CheckSpaceAvailable(AudioPriorityType audioPriorityType)
        {
            return _priorityByLimitPlay[audioPriorityType] > _priorityByAmountPlaying[audioPriorityType];
        }

        public void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer)
        {
            _priorityByAmountPlaying[audioPriorityType] += 1;
            _audioPlayingByAudioPriority[audioPlayer] = audioPriorityType;
        }

        public void RemoveAudioPlayer(IAudioPlayer audioPlayer)
        {
            var priorityType = _audioPlayingByAudioPriority[audioPlayer];
            _audioPlayingByAudioPriority.Remove(audioPlayer);
            _priorityByAmountPlaying[priorityType] -= 1;
        }
    }
}