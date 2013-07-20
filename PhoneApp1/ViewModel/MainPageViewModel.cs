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
        private string _buttonText = "Start";

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

        public string ButtonText
        {
            get { return _buttonText; }
            set 
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                }
                OnPropertyChanged("ButtonText");
            }
        }

        public MainPageViewModel()
        { }

        public ICommand StartButtonClick
        {
            get { return new DelegateCommand(ToggleStartAndStopButton, CanExecute); }
        }

        public bool CanExecute(object returnsTrue)
        {
            return true;
        }

        public void StopCounter()
        {
            _dispatcherTimer.Stop();
            _stopWatch.ButtonText = "Start";
            ButtonText = _stopWatch.ButtonText;
        }

        public void StartCounter()
        {
            _stopWatch = new StopWatch();
            _stopWatch.Seconds = 0;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
            _stopWatch.ButtonText = "Stop";
            ButtonText = _stopWatch.ButtonText;
        }

        public void ToggleStartAndStopButton(object something)
        {
            if (ButtonText == "Start")
            {
                StartCounter();
            }
            else if (ButtonText == "Stop")
            {
                StopCounter();
            }
        }
        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            _stopWatch.Seconds += 1;
            Seconds = _stopWatch.Seconds;
        }

    }
}