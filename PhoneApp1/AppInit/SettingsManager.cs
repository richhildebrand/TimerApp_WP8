using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace TimerUI.AppInit
{
    public static class SettingsManager
    {
        public static void ApplyDefaultsToAnyUnsetValues()
        {
            SetToDefaultIfUnset<TimeSpan>("VoiceTimeout", TimeSpan.FromMinutes(6));
            SetToDefaultIfUnset<String>("StartVoiceCommands", "Start");
            SetToDefaultIfUnset<String>("StopVoiceCommands", "Stop");
        }

        private static void SetToDefaultIfUnset<T>(string setting, T defaultValue)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(setting))
            {
                IsolatedStorageSettings.ApplicationSettings.Add(setting, defaultValue);
            }
        }

        public static T Get<T>(string setting)
        {
            return (T)IsolatedStorageSettings.ApplicationSettings[setting];
        }

        public static void Set<T>(string setting, T value)
        {
            IsolatedStorageSettings.ApplicationSettings[setting] = value;
        }
    }
}
