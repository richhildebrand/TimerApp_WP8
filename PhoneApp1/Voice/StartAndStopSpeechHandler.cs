using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            var thing = 0;
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
            else if (input == "Lap")
            {
                _messenger.Publish(new StopwatchLapEvent());
                Speech.Synthesizer.SpeakTextAsync("New lap started.");
            }
            else if (input == "Clear")
            {
                _messenger.Publish(new StopwatchResetEvent());
            }
            StartListening();
        }

        public async void StartListening()
        {
            try
            {
                var result = await Speech.Recognizer.RecognizeAsync();
                HandleInput(result.Text);
            }
            catch { } // voice is not active on the phone
        }
    }
}
