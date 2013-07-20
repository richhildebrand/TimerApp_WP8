using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using TimerUI.Models;
using TimerUI.ViewModels.Commands;
using GalaSoft.MvvmLight;

namespace TimerUI.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private StopWatch _stopWatch;
        private string _buttonText = "Start";

        public MainPageViewModel()
        {
            _stopWatch = new StopWatch();
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
                RaisePropertyChanged("ButtonText");
            }
        }

        public ICommand StartButtonClick
        {
            get { return new DelegateCommand(ToggleStartAndStopButton, ReturnsTrue); }
        }

        public bool ReturnsTrue(object notUsed)
        {
            return true;
        }

        public bool CanExecute(object returnsTrue)
        {
            return true;
        }

        public void StopCounter()
        {
            _stopWatch.Stop();
            ButtonText = "Start";
        }

        public void StartCounter()
        {
            _stopWatch.Start();
            ButtonText = "Stop";
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
    }
}