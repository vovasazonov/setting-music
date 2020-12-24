using System;
using System.Collections.Generic;

namespace Audio
{
    [Serializable]
    public struct LimitAudioPriority
    {
        public int LimitImpotant;
        public int LimitUnimpotant;
        public int LimitLeast;

        public Dictionary<AudioPriorityType, int> ConvertToDictionary()
        {
            return new Dictionary<AudioPriorityType, int>
            {
                {AudioPriorityType.Important, LimitImpotant},
                {AudioPriorityType.Unimportant, LimitUnimpotant},
                {AudioPriorityType.Least, LimitLeast}
            };
        }
    }
}