﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Telerik.Windows.Controls;
using TimerUI.AppInit;
using TimerUI.Helpers;
using TimerUI.Messages;
using TimerUI.Voice;

namespace TimerUI.ViewModels
{
    public class MainPageViewModel : Screen, IHandle<StopwatchTickEvent>, IHandle<StopwatchStartEvent>, IHandle<StopwatchStopEvent>
    {
        private readonly TimeFormatter _timeFormatter = new TimeFormatter();
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _messenger;
        private readonly CustomStopwatch _stopWatch;

        private List<RecognizableString>_validVoiceCommands;
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
            TotalTimeElapsed = "";

            Speech.Initialize();
            string commands = "";
            var voiceCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StartVoiceCommands)
                .Union(SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StopVoiceCommands)).ToList();

            voiceCommands.ForEach(sc => commands = commands + ", " + sc.Value);
            Speech.Synthesizer.SpeakTextAsync("Current voice commands are set to " + commands);

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.Container.GetAllInstances(typeof(IEventAggregator))
                                                                     .FirstOrDefault() as IEventAggregator;
            _messenger.Subscribe(this);
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            ButtonText = "Start";
            ValidVoiceCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StartVoiceCommands);
            SpeechManager.StartListening();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            if (_stopWatch.IsRunning())
            {
                Handle(new StopwatchStopEvent());
            }
            SpeechManager.Reset();
        }

        public void Handle(StopwatchStopEvent message)
        {
            if (_stopWatch.IsRunning())
            {
                _stopWatch.Stop();
                ButtonText = "Start";
                AddLapTimeToList();
                AddTotalTime();
                ValidVoiceCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StartVoiceCommands);
            }
        }

        public void Handle(StopwatchStartEvent message)
        {
            if (!_stopWatch.IsRunning())
            {
                _stopWatch.Reset();
                _stopWatch.Start();
                ButtonText = "Stop";
                ValidVoiceCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StopVoiceCommands);
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

        public List<RecognizableString> ValidVoiceCommands
        {
            get { return _validVoiceCommands; }
            set { _validVoiceCommands = value; NotifyOfPropertyChange(() => ValidVoiceCommands); }
        }

        public string CurrentLap
        {
            get { return _currentLap; }
            set { _currentLap = value; NotifyOfPropertyChange(() => CurrentLap); }
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
            set { _totalTimeElapsed = value; NotifyOfPropertyChange(() => TotalTimeElapsed); }
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
            _previousMilli += _actualMilliseconds;
            var temp = new List<string>();
            var lapEntry = "Lap " + (ListOfLapTimes.Count + 1) + " - " + Milliseconds;
            temp.Add(lapEntry);
            ListOfLapTimes = temp.Union(ListOfLapTimes).ToList();
        }

        public void AddTotalTime()
        {
            long totalSeconds = _previousMilli;
            TotalTimeElapsed = "Total Time: " +_timeFormatter.FormatMilliseconds(totalSeconds);
        }

        public void ResetAllTimes()
        {
            Handle(new StopwatchStopEvent());
            TotalTimeElapsed = "";
            ListOfLapTimes = new List<string>();
            Milliseconds = "0";
        }
    }
}