using System;
using System.Windows.Threading;
using TimerUI.Models;
using TimerUI.ViewModels;

namespace TimerUI.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private DispatcherTimer _dispatcherTimer;
        private StopWatch _stopWatch;
        private int _seconds;

        public int Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                if (_seconds != value)
                {
                    _seconds = value;
                }
                OnPropertyChanged("Seconds");
            }
        }

        public MainPageViewModel()
        {
            _stopWatch = new StopWatch();
            _stopWatch.Seconds = 0;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }



        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _stopWatch.Seconds += 1;
            Seconds = _stopWatch.Seconds;
        }

    }
}