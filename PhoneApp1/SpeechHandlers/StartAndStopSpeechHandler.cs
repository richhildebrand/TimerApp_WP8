using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace TimerUI.SpeechHandlers
{
    public class StartAndStopSpeechHandler : ISpeechInputHandler
    {
        private readonly string START_TIMER = "Start";
        private readonly string STOP_TIMER = "Stop";
        private readonly CustomStopwatch _stopWatch;

        public StartAndStopSpeechHandler(CustomStopwatch stopWatch)
        {
            _stopWatch = stopWatch;
        }

        public StartAndStopSpeechHandler() : this(new CustomStopwatch()) { }

        public bool CanHandleInput(string input)
        {
            return true;
        }

        public void HandleInput(FrameworkElement target, string input)
        {
            if (string.Compare(START_TIMER, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                _stopWatch.Start();
                Telerik.Windows.Controls.SpeechManager.StartListening();
            }

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
