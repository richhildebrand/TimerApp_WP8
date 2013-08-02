using System;
using System.Linq;

namespace TimerUI.Messages
{
    public class StopwatchTickEvent
    {
        public long Miliseconds { get; set; }

        public StopwatchTickEvent(long miliseconds)
        {
            Miliseconds = miliseconds;
        }
    }
}
