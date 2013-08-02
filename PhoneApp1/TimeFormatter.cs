using System;
using System.Linq;

namespace TimerUI
{
    public class TimeFormatter
    {
        private static readonly int ONE_SECOND = 1000;
        private static readonly int ONE_MINUTE = 60000;

        public string FormatMiliseconds(long miliseconds)
        {


            string displayTime;
            TimeSpan t = TimeSpan.FromMilliseconds(miliseconds);
            if (miliseconds < ONE_SECOND)
            {
                displayTime = miliseconds.ToString();
            }
            else if (miliseconds < ONE_MINUTE)
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
