using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using TimerUI.AppInit;

namespace TimerUI.Helpers
{
    class SpeechEvaluator
    {
        public bool IsValidStartCommand(string possibleStartCommand)
        {
            var startCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StartVoiceCommands);
            return startCommands.Any(sc => AreEqual(sc.Value, possibleStartCommand));
        }

        public bool IsValidStopCommand(string possibleStopCommand)
        {
            var startCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StopVoiceCommands);
            return startCommands.Any(sc => AreEqual(sc.Value, possibleStopCommand));
        }

        private bool AreEqual(string validCommand, string possibleCommand)
        {
            return string.Compare(validCommand, possibleCommand, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}
