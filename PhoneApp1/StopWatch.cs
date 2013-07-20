using System;
using System.Linq;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;

namespace TimerUI
{
    public class StopWatch
    {
        private readonly DispatcherTimer _timer;

        public int Seconds { get; set; }

        public StopWatch()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += OnEachTick;
            _timer.Interval = new TimeSpan(0, 0, 1);
            
        }

        private void OnEachTick(object sender, EventArgs e)
        {
            Seconds += 1;
            Messenger.Default.Send<StopWatch>(this);
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
