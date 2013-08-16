using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Telerik.Windows.Controls;
using TimerUI.AppInit;
using TimerUI.Messages;

namespace TimerUI.Voice
{
    public class StartAndStopSpeechHandler : ISpeechInputHandler
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

        public bool CanHandleInput(string input)
        {
            return true;
        }

        public void HandleInput(FrameworkElement target, string input)
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
            SpeechManager.StartListening();
        }

        public void NotifyInputError(FrameworkElement target)
        {
            MessageBox.Show("Error");
        }
    }
}
