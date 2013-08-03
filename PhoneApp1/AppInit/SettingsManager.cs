using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace TimerUI.AppInit
{
    public class SettingsManager
    {
        public void ApplyDefaultsToAnyUnsetValues()
        {
            SetToDefaultIfUnset<TimeSpan>("VoiceTimeout", TimeSpan.FromMinutes(6));
            SetToDefaultIfUnset<String>("StartVoiceCommands", "Start");
            SetToDefaultIfUnset<String>("StopVoiceCommands", "Stop");
        }

        private void SetToDefaultIfUnset<T>(string setting, T defaultValue)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(setting))
            {
                IsolatedStorageSettings.ApplicationSettings.Add(setting, defaultValue);
            }
        }
    }
}
