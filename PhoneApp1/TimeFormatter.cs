using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerUI
{
    public class TimeFormatter
    {
        public string FormatMiliseconds(long miliseconds)
        {
            string displayTime;
            TimeSpan t = TimeSpan.FromMilliseconds(miliseconds);
            if (miliseconds < 1000)
            {
                displayTime = miliseconds.ToString();
            }
            else if (miliseconds < 60000)
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
