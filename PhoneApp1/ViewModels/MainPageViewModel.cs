using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using TimerUI.AppInit;
using TimerUI.Messages;
using TimerUI.Voice;

namespace TimerUI.ViewModels
{
    public class MainPageViewModel : PropertyChangedBase, IHandle<StopwatchTickEvent>, IHandle<StopwatchStartEvent>, IHandle<StopwatchStopEvent>
    {
        private readonly TimeFormatter _timeFormatter = new TimeFormatter();
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _messenger;
        private readonly CustomStopwatch _stopWatch;

        private string _milliseconds;
        private string _buttonText;
        private string _addItemText;
        private Uri _icon;
        private bool _isVisible;
        private string _totalTimeText;        

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _stopWatch = new CustomStopwatch();
            Milliseconds = "0";
            ButtonText = "Start";
            AddItemText = "Timeout Settings";
            Icon = new Uri("/Images/appbar.settings.png", UriKind.Relative);
            IsVisible = true;
            TotalTimeLabel = "Total Lap:";

            Speech.Initialize();

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.Container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
            _messenger.Subscribe(this);
        }

        public void Handle(StopwatchStopEvent message)
        {
            if (_stopWatch.IsRunning())
            {
                _stopWatch.Stop();
                ButtonText = "Start";
                AddLapTimeToList();
                Telerik.Windows.Controls.SpeechManager.StartListening();
            }
        }

        public void Handle(StopwatchStartEvent message)
        {
            if (!_stopWatch.IsRunning())
            {
                _stopWatch.Reset();
                _stopWatch.Start();
                ButtonText = "Stop";
                Telerik.Windows.Controls.SpeechManager.StartListening();
            }
        }

        public void NavigateToSettingsPage()
        {
            _navigationService.UriFor<SettingsPageViewModel>().Navigate();
        }

        public void Handle(StopwatchTickEvent stopwatchTick)
        {
            Milliseconds = _timeFormatter.FormatMilliseconds(stopwatchTick.Milliseconds);
        }

        public string Milliseconds
        { 
            get { return this._milliseconds; }
            set { _milliseconds = value; NotifyOfPropertyChange(() => Milliseconds); }
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

        public string TotalTimeLabel
        {
            get { return this._totalTimeText; }
            set { _totalTimeText = value; NotifyOfPropertyChange(() => TotalTimeLabel); }
        }

        public void ToggleStartAndStopButton(object sender)
        {
            if (ButtonText == "Start")
            {
                _messenger.Publish(new StopwatchStartEvent());
            }
            else if (ButtonText == "Stop")
            {
                _messenger.Publish(new StopwatchStopEvent());
                Telerik.Windows.Controls.SpeechManager.StartListening();
            }
        }

        public void AddLapTimeToList()
        {
            TotalTimeLabel += Milliseconds;
        }
    }
}