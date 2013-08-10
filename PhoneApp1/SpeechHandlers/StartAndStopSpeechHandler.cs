using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Telerik.Windows.Controls;
using TimerUI.AppInit;
using TimerUI.Messages;

namespace TimerUI.SpeechHandlers
{
    public class StartAndStopSpeechHandler : ISpeechInputHandler
    {
        private static readonly string START_TIMER = "Start";
        private static readonly string STOP_TIMER = "Stop";
        private readonly IEventAggregator _messenger;

        public StartAndStopSpeechHandler() 
        {
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
            if (string.Compare(START_TIMER, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                _messenger.Publish(new StopwatchStartEvent());
            }

            if (string.Compare(STOP_TIMER, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                _messenger.Publish(new StopwatchStopEvent());
            }
            SpeechManager.StartListening();
        }

        public void NotifyInputError(FrameworkElement target)
        {
            MessageBox.Show("Error");
        }
    }
}
