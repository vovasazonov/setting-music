using System;
using System.Collections.Generic;

namespace Audio
{
    internal sealed class AmountPriorityController : IAmountPriorityController
    {
        private readonly IDictionary<AudioPriorityType, int> _priorityByAmountPlaying = new Dictionary<AudioPriorityType, int>();
        private readonly IDictionary<IAudioPlayer, AudioPriorityType> _audioPlayingByAudioPriority = new Dictionary<IAudioPlayer, AudioPriorityType>();
        private readonly IDictionary<AudioPriorityType, int> _priorityByLimitPlay = new Dictionary<AudioPriorityType, int>();

        public AmountPriorityController(IReadOnlyDictionary<AudioPriorityType, int> priorityByLimitPlay)
        {
            InitializeToDefault();
            UpdateLimitPlay(priorityByLimitPlay);
        }

        private void InitializeToDefault()
        {
            foreach (AudioPriorityType priority in Enum.GetValues(typeof(AudioPriorityType)))
            {
                _priorityByAmountPlaying.Add(priority, 0);
                _priorityByLimitPlay.Add(priority, 0);
            }
        }

        private void UpdateLimitPlay(IReadOnlyDictionary<AudioPriorityType, int> priorityByLimitPlay)
        {
            foreach (var priority in priorityByLimitPlay.Keys)
            {
                var limitPlay = priorityByLimitPlay[priority];
                _priorityByLimitPlay[priority] = limitPlay;
            }
        }

        public bool CheckSpaceAvailable(AudioPriorityType audioPriorityType)
        {
            var limitPlay = _priorityByLimitPlay[audioPriorityType];
            var amountPlaying = _priorityByAmountPlaying[audioPriorityType];

            return limitPlay > amountPlaying;
        }

        public void AddAudioPlayer(AudioPriorityType audioPriorityType, IAudioPlayer audioPlayer)
        {
            _priorityByAmountPlaying[audioPriorityType] += 1;
            _audioPlayingByAudioPriority.Add(audioPlayer, audioPriorityType);
        }

        public void RemoveAudioPlayer(IAudioPlayer audioPlayer)
        {
            var priorityType = _audioPlayingByAudioPriority[audioPlayer];
            _audioPlayingByAudioPriority.Remove(audioPlayer);
            _priorityByAmountPlaying[priorityType] -= 1;
        }
    }
}