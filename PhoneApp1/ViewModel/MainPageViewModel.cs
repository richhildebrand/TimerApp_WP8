using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using TimerUI.ViewModels.Commands;
using GalaSoft.MvvmLight;

namespace TimerUI.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly StopWatch _stopWatch;
        private string _buttonText = "Start";
        private int _seconds;

        public MainPageViewModel()
        {
            _stopWatch = new StopWatch();
            Messenger.Default.Register<StopWatch>(this, OnStopWatchTick);
        }

        private void OnStopWatchTick(StopWatch stopWatch)
        {
            Seconds = stopWatch.Seconds;
        }

        public int Seconds
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