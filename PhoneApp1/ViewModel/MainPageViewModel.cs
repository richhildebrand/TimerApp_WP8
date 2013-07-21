using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using TimerUI.Interfaces;
using TimerUI.ViewModels.Commands;
using GalaSoft.MvvmLight;

namespace TimerUI.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private TimeFormatter _timeFormatter = new TimeFormatter();
        private readonly IStopWatch _stopWatch;
        private string _buttonText = "Start";
        private string _seconds;

        public MainPageViewModel(IStopWatch stopWatch)
        {
            _stopWatch = stopWatch;
            Seconds = "0";
            Messenger.Default.Register<StopWatch>(this, OnStopWatchTick);
        }

        public MainPageViewModel() : this(new StopWatch())
        {
        }

        private void OnStopWatchTick(StopWatch stopWatch)
        {
            Seconds = _timeFormatter.FormatSeconds(stopWatch.Seconds);
        }

        public string Seconds
        {
            get { return _seconds; }
            set { _seconds = value; RaisePropertyChanged("Seconds"); }
        }

        public string ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value; RaisePropertyChanged("ButtonText"); }
        }

        public ICommand StartButtonClick
        {
            get { return new DelegateCommand(ToggleStartAndStopButton, CanExecute); }
        }

        public bool CanExecute(object returnsTrue)
        {
            return true;
        }

        public void ToggleStartAndStopButton(object sender)
        {
            if (ButtonText == "Start")
            {
                _stopWatch.Start();
                ButtonText = "Stop";
            }
            else if (ButtonText == "Stop")
            {
                _stopWatch.Stop();
                ButtonText = "Start";
            }
        }
    }
}