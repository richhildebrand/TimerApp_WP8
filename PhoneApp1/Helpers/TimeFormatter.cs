using System;
using System.Linq;

namespace TimerUI.Helpers
{
    public class TimeFormatter
    {
        private static readonly int ONE_SECOND = 1000;
        private static readonly int ONE_MINUTE = 60000;

        public string FormatMilliseconds(long milliseconds)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(milliseconds);
            if (milliseconds < ONE_SECOND)
            {
                return milliseconds.ToString();
            }

            else if (milliseconds < ONE_MINUTE)
            {
                return string.Format("{0:D1}:{1:D3}", t.Seconds, t.Milliseconds);
            }
            else
            {
                return string.Format("{0:D1}:{1:D2}:{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
            }
        }
    }
}
