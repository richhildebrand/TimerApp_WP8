using System;
using System.Windows.Input;
using System.Windows.Threading;
using TimerUI.Models;
using TimerUI.ViewModels;
using TimerUI.ViewModels.Commands;

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
        }

        public ICommand StartButtonClick
        {
            get { return new DelegateCommand(StartCounter, WitchCraft); }
        }

        public bool WitchCraft(object flyingBroom)
        {
            return true;
        }

        public void StartCounter(object fireBall)
        {
            _stopWatch = new StopWatch();
            _stopWatch.Seconds = 0;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            _stopWatch.Seconds += 1;
            Seconds = _stopWatch.Seconds;
        }

    }
}