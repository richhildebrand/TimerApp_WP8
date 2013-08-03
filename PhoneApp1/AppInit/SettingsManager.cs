using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace TimerUI.AppInit
{
    public static class SettingsManager
    {
        public enum Settings
        {
            VoiceTimeout,
            StartVoiceCommands,
            StopVoiceCommands
        };

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

        public static T Get<T>(Settings setting)
        {
            return (T)IsolatedStorageSettings.ApplicationSettings[setting.ToString()];
        }

        public static void Set<T>(Settings setting, T value)
        {
            IsolatedStorageSettings.ApplicationSettings[setting.ToString()] = value;
        }
    }
}
