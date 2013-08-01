using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerUI
{
    public class TimeFormatter
    {
        public string FormatSeconds(int seconds)
        {
            if (seconds >= 60)
            {
                TimeSpan t = TimeSpan.FromSeconds(seconds);
                return string.Format("{0:D1}:{1:D2}", t.Minutes, t.Seconds);
            }
            return seconds.ToString();
        }
    }
}
