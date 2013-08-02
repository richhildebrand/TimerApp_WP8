using System;
using System.Linq;

namespace TimerUI.Messages
{
    public class StopwatchTickEvent
    {
        public long Milliseconds { get; set; }

        public StopwatchTickEvent(long milliseconds)
        {
            Milliseconds = milliseconds;
        }
    }
}
