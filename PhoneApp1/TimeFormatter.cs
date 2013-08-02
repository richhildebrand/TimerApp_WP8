using System;
using System.Linq;

namespace TimerUI
{
    public class TimeFormatter
    {
        private static readonly int ONE_SECOND = 1000;
        private static readonly int ONE_MINUTE = 60000;

        public string FormatMilliseconds(long milliseconds)
        {


            string displayTime;

            TimeSpan t = TimeSpan.FromMilliseconds(milliseconds);
            if (milliseconds < ONE_SECOND)
            {
                displayTime = milliseconds.ToString();
            }

            else if (milliseconds < ONE_MINUTE)
            {
                displayTime = string.Format("{0:D1}:{1:D1}", t.Seconds, t.Milliseconds);
            }
            else
            {
                displayTime = string.Format("{0:D1}:{1:D2}:{2:D2}", t.Minutes, t.Seconds, t.Milliseconds);
            }
            return StripLastZero(displayTime);
        }

        private string StripLastZero(string displayTimeWithExtraZero)
        {
            return displayTimeWithExtraZero.Substring(0, displayTimeWithExtraZero.Length - 1);
        }
    }
}
