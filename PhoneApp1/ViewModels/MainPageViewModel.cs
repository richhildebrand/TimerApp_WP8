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
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _messenger;
        private readonly IStopWatch _stopWatch;

        private string miliseconds;
        private string _buttonText;
        private string _addItemText;
        private Uri _icon;
        private bool _isVisible;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _stopWatch = new StopWatch();
            Miliseconds = "0";
            ButtonText = "Start";
            AddItemText = "Timeout Settings";
            Icon = new Uri("/Images/appbar.settings.png", UriKind.Relative);
            IsVisible = true; 

            Speech.Initialize();

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
            _messenger.Subscribe(this);
        }

        public void NavigateToSettingsPage()
        {
            _navigationService.UriFor<SettingsPageViewModel>().Navigate();
        }

        public void Handle(StopwatchTickEvent stopwatchTick)
        {
            Miliseconds = _timeFormatter.FormatSeconds(stopwatchTick.Seconds);
        }

        public string Miliseconds
        { 
            get { return this.miliseconds; }
            set { miliseconds = value; NotifyOfPropertyChange(() => Miliseconds); }
        }

        public string ButtonText
        {
            get { return this._buttonText; }
            set { _buttonText = value; NotifyOfPropertyChange(() => ButtonText); }
        }

        public string AddItemText
        {
            get { return _addItemText; }
            set { _addItemText = value; NotifyOfPropertyChange(() => AddItemText); }
        }

        public Uri Icon
        {
            get { return _icon; }
            set { _icon = value; NotifyOfPropertyChange(() => Icon); }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; NotifyOfPropertyChange(() => IsVisible); }
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