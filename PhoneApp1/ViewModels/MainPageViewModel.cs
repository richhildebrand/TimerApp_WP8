using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using TimerUI.AppInit;
using TimerUI.Helpers;
using TimerUI.Messages;
using TimerUI.Voice;

namespace TimerUI.ViewModels
{
    public class MainPageViewModel : Screen, IHandle<StopwatchTickEvent>, IHandle<StopwatchStartEvent>, IHandle<StopwatchStopEvent>, IHandle<StopwatchLapEvent>
    {
        private readonly TimeFormatter _timeFormatter = new TimeFormatter();
        private readonly StartAndStopSpeechHandler _speechHandler = new StartAndStopSpeechHandler();
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _messenger;
        private readonly CustomStopwatch _stopWatch;

        private Visibility _showLapButton;
        private string _milliseconds;
        private string _currentLap;
        private string _buttonText;
        private string _addItemText;
        private Uri _icon;
        private bool _isVisible;
        private List<string> _listOfLapTimes;        

        private string _totalTimeElapsed;

        private long _actualMilliseconds;
        private long _previousMilli;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _stopWatch = new CustomStopwatch();
            Milliseconds = "0";

            AddItemText = "Timeout Settings";
            Icon = new Uri("/Images/appbar.settings.png", UriKind.Relative);
            IsVisible = true;
            ListOfLapTimes = new List<string>();
            TotalTimeElapsed = "0";

            Speech.Initialize();
            string commands = "";
            var voiceCommands = SettingsManager.Get<List<string>>(SettingsManager.Settings.StartVoiceCommands)
                .Union(SettingsManager.Get<List<string>>(SettingsManager.Settings.StopVoiceCommands)).ToList();

            voiceCommands.ForEach(sc => commands = commands + ", " + sc);
            Speech.Synthesizer.SpeakTextAsync("Current voice commands are set to " + commands);

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.Container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
            _messenger.Subscribe(this);
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            ShowLapButton = Visibility.Collapsed;
            ButtonText = "Start";
            Speech.Recognizer.Grammars["Start"].Enabled = true;
            Speech.Recognizer.Grammars["Lap"].Enabled = false;
            Speech.Recognizer.Grammars["Stop"].Enabled = false;
            _speechHandler.StartListening();
        }



        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            if (_stopWatch.IsRunning())
            {
                Handle(new StopwatchStopEvent());
            }
        }

        public void Handle(StopwatchStopEvent message)
        {
            if (_stopWatch.IsRunning())
            {
                _stopWatch.Stop();
                Speech.Recognizer.Grammars["Start"].Enabled = true;
                Speech.Recognizer.Grammars["Lap"].Enabled = false;
                Speech.Recognizer.Grammars["Stop"].Enabled = false;
                ShowLapButton = Visibility.Collapsed;
                ButtonText = "Start";
                AddLapTimeToList();
                AddTotalTime();
            }
        }

        public void Handle(StopwatchStartEvent message)
        {
            if (!_stopWatch.IsRunning())
            {
                _stopWatch.Start();
                Speech.Recognizer.Grammars["Start"].Enabled = false;
                Speech.Recognizer.Grammars["Lap"].Enabled = true;
                Speech.Recognizer.Grammars["Stop"].Enabled = true;
                ShowLapButton = Visibility.Visible;
                ButtonText = "Stop";
            }
        }

        public void Handle(StopwatchLapEvent message)
        {
            if (_stopWatch.IsRunning())
            {
                Handle(new StopwatchStopEvent());
                Handle(new StopwatchStartEvent());

            }
        }

        public void NavigateToSettingsPage()
        {
            _navigationService.UriFor<SettingsPageViewModel>().Navigate();
        }

        public void Handle(StopwatchTickEvent stopwatchTick)
        {
            _actualMilliseconds = stopwatchTick.Milliseconds;
            Milliseconds =  _timeFormatter.FormatMilliseconds(stopwatchTick.Milliseconds);
            CurrentLap = "Current Lap - " + Milliseconds;
        }

        public string CurrentLap
        {
            get { return _currentLap; }
            set { _currentLap = value; NotifyOfPropertyChange(() => CurrentLap); }
        }

        public Visibility ShowLapButton
        {
            get { return _showLapButton; }
            set { _showLapButton = value; NotifyOfPropertyChange(() => ShowLapButton); }
        }

        public string Milliseconds
        { 
            get { return _milliseconds; }
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

        public List<string> ListOfLapTimes
        {
            get { return this._listOfLapTimes; }
            set { _listOfLapTimes = value; NotifyOfPropertyChange(() => ListOfLapTimes); }
        }

        public string TotalTimeElapsed
        {
            get { return this._totalTimeElapsed; }
            set { _totalTimeElapsed = "Total Time: " + value; NotifyOfPropertyChange(() => TotalTimeElapsed); }
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

        public void LapButton(object sender)
        {
            Handle(new StopwatchLapEvent());
        }

        public void AddLapTimeToList()
        {
            _previousMilli += _actualMilliseconds;
            var temp = new List<string>();
            var lapEntry = "Lap " + (ListOfLapTimes.Count + 1) + " - " + Milliseconds;
            temp.Add(lapEntry);
            ListOfLapTimes = temp.Union(ListOfLapTimes).ToList();
        }

        public void AddTotalTime()
        {
            long totalSeconds = _previousMilli;
            TotalTimeElapsed = _timeFormatter.FormatMilliseconds(totalSeconds);
        }

        public void ResetAllTimes()
        {
            Handle(new StopwatchStopEvent());
            CurrentLap = "";
            TotalTimeElapsed = "";
            ListOfLapTimes = new List<string>();
            Milliseconds = "0";
            TotalTimeElapsed = "0";
            _previousMilli = 0;
        }
    }
}