using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace TimerUI.Models
{
    public class StopWatch
    {
        private DispatcherTimer _timer;
        private int _seconds;

        public int Seconds
        {
            get
            {
                return _seconds;
            }
            set { _seconds = value; }
        }

        public StopWatch()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += OnEachTick;
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void OnEachTick(object sender, EventArgs e)
        {
            Seconds += 1;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
