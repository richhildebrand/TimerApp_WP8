using System;
using System.Linq;

namespace TimerUI.Messages
{
    public class StopwatchTickEvent
    {
        public int Seconds { get; set; }

        public StopwatchTickEvent(int seconds)
        {
            Seconds = seconds;
        }
    }
}
