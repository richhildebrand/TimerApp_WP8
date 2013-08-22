using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using TimerUI.AppInit;

namespace TimerUI.Voice
{
    class SpeechEvaluator
    {
        public bool IsValidStartCommand(string possibleStartCommand)
        {
            var startCommands = SettingsManager.Get<List<string>>(SettingsManager.Settings.StartVoiceCommands);
            return startCommands.Any(sc => AreEqual(sc, possibleStartCommand));
        }

        public bool IsValidStopCommand(string possibleStopCommand)
        {
            var startCommands = SettingsManager.Get<List<string>>(SettingsManager.Settings.StopVoiceCommands);
            return startCommands.Any(sc => AreEqual(sc, possibleStopCommand));
        }

        private bool AreEqual(string validCommand, string possibleCommand)
        {
            return string.Compare(validCommand, possibleCommand, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}
