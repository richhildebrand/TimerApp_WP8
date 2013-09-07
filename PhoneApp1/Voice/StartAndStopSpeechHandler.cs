using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using TimerUI.AppInit;
using TimerUI.Messages;

namespace TimerUI.Voice
{
    public class StartAndStopSpeechHandler
    {
        private readonly IEventAggregator _messenger;
        private readonly SpeechEvaluator _speechEvaluator;

        public StartAndStopSpeechHandler() 
        {
             _speechEvaluator = new SpeechEvaluator();

            Bootstrapper bootstrapper = Application.Current.Resources["bootstrapper"] as Bootstrapper;
            _messenger = bootstrapper.Container
                                     .GetAllInstances(typeof(IEventAggregator))
                                     .FirstOrDefault() as IEventAggregator;
        }

        public void HandleInput(string input)
        {
            if (_speechEvaluator.IsValidStartCommand(input))
            {
                _messenger.Publish(new StopwatchStartEvent());
                Speech.Synthesizer.SpeakTextAsync("Timer started.");
            }
            else if (_speechEvaluator.IsValidStopCommand(input))
            {
                _messenger.Publish(new StopwatchStopEvent());
                Speech.Synthesizer.SpeakTextAsync("Timer stopped.");
            }
            StartListening();
        }

        public async void StartListening()
        {
            var result = await Speech.Recognizer.RecognizeAsync();
            HandleInput(result.Text);
        }
    }
}
