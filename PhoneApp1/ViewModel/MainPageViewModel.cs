using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using TimerUI.Interfaces;
using TimerUI.ViewModels.Commands;
using GalaSoft.MvvmLight;
using TimerUI.Voice;
using Windows.Phone.Speech.Recognition;
using Microsoft.Phone.Controls;

namespace TimerUI.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly TimeFormatter _timeFormatter = new TimeFormatter();
        private readonly IStopWatch _stopWatch;
        private string _buttonText = "Start";
        private string _seconds;

        public MainPageViewModel(IStopWatch stopWatch)
        {
            _stopWatch = stopWatch;
            Seconds = "0";
            Messenger.Default.Register<StopWatch>(this, OnStopWatchTick);
            Speech.Initialize();
        }

        public MainPageViewModel() : this(new StopWatch())
        {
        }

        private async void ListenForStartCommand(object sender)
        {
            Speech.RecognizerUI.Recognizer.Grammars["Stop"].Enabled = false;
            Speech.RecognizerUI.Recognizer.Grammars["Start"].Enabled = true;
            
            Speech.RecognizerUI.Settings.ListenText = @"Say 'Start' to start the stopwatch.";
            SpeechRecognitionUIResult result = await Speech.RecognizerUI.RecognizeWithUIAsync();
            if (result.ResultStatus == SpeechRecognitionUIStatus.Succeeded && result.RecognitionResult.Text.Contains("Start"))
            {
                _stopWatch.Start();
                await Speech.Synthesizer.SpeakTextAsync("Timer Started");
                ListenForStopCommand(sender);
            }
            else { ListenForStartCommand(sender); }
        }

        private async void ListenForStopCommand(object sender)
        {
            Speech.RecognizerUI.Recognizer.Grammars["Stop"].Enabled = true;
            Speech.RecognizerUI.Recognizer.Grammars["Start"].Enabled = false;

            Speech.RecognizerUI.Settings.ListenText = @"Say 'Stop' to stop the stopwatch.";
            SpeechRecognitionUIResult result = await Speech.RecognizerUI.RecognizeWithUIAsync();
            if (result.ResultStatus == SpeechRecognitionUIStatus.Succeeded && result.RecognitionResult.Text.Contains("Stop"))
            {
                _stopWatch.Stop();
                await Speech.Synthesizer.SpeakTextAsync("Timer stopped at " + Seconds + " seconds.");
            }
            else { ListenForStopCommand(sender); }
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

        public ICommand ActivateVoiceCommandsClick
        {
            get { return new DelegateCommand(ListenForStartCommand, CanExecute); }
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