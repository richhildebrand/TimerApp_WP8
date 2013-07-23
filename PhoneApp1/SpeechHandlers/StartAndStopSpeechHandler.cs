using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using TimerUI.Interfaces;
using TimerUI.ViewModel;
using TimerUI.Voice;

namespace TimerUI.SpeechHandlers
{
    public class StartAndStopSpeechHandler : ISpeechInputHandler
    {
        private const string START_TIMER = "Start";
        private const string STOP_TIMER = "Stop";

        public bool CanHandleInput(string input)
        {
            return true;
        }

        private StopWatch _stopWatch;

        public StartAndStopSpeechHandler()
        {
            _stopWatch = new StopWatch();
        }

        public void HandleInput(FrameworkElement target, string input)
        {
            if (string.Compare(START_TIMER, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                _stopWatch.Start();
                Telerik.Windows.Controls.SpeechManager.StartListening();
            }

            //Make this work.....
            if (string.Compare(STOP_TIMER, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                _stopWatch.Stop();
                Telerik.Windows.Controls.SpeechManager.StartListening();
            }
        }

        public void NotifyInputError(FrameworkElement target)
        {
            MessageBox.Show("Error");

        }
    }
}
