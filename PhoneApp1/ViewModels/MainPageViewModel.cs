using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using TimerUI.Interfaces;
using TimerUI.Messages;
using TimerUI.Voice;

namespace TimerUI.ViewModels
{
    public class MainPageViewModel : PropertyChangedBase, IHandle<StopwatchTickEvent>
    {
        private readonly TimeFormatter _timeFormatter = new TimeFormatter();
        private readonly IEventAggregator _messenger;
        private readonly IStopWatch _stopWatch;

        private string _seconds;
        private string _buttonText;

        public MainPageViewModel(INavigationService nav)
        {
            _stopWatch = new StopWatch();
            Seconds = "0";
            ButtonText = "Start";

            Speech.Initialize();

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
            _messenger.Subscribe(this);
        }

        public void Handle(StopwatchTickEvent stopwatchTick)
        {
            Seconds = _timeFormatter.FormatSeconds(stopwatchTick.Seconds);
        }

        public string Seconds
        { 
            get { return this._seconds; }
            set { _seconds = value; NotifyOfPropertyChange(() => Seconds); }
        }

        public string ButtonText
        {
            get { return this._buttonText; }
            set { _buttonText = value; NotifyOfPropertyChange(() => ButtonText); }
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