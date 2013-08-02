using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerUI
{
    public class TimeFormatter
    {
        public string FormatSeconds(int miliseconds)
        {
            string displayTime;
            if (miliseconds >= 1000)
            {

                TimeSpan t = TimeSpan.FromMilliseconds(miliseconds);
                displayTime = string.Format("{0:D1}:{1:D1}", t.Seconds, t.Milliseconds);
                return StripLastZero(displayTime);
            }
            displayTime = miliseconds.ToString();
            return StripLastZero(displayTime);
        }

        private string StripLastZero(string displayTimeWithExtraZero)
        {
            return displayTimeWithExtraZero.Substring(0, displayTimeWithExtraZero.Length - 1);
        }
    }
}
