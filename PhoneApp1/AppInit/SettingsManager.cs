using System;
using System.Collections.Generic;
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

            var startCommands = new List<string>();
            startCommands.Add("Start");
            SetToDefaultIfUnset<List<string>>("StartVoiceCommands", startCommands);

            var stopCommands = new List<string>();
            stopCommands.Add("Stop");
            SetToDefaultIfUnset<List<string>>("StopVoiceCommands", stopCommands);
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

        public static void AddNewVoiceCommand(Settings voiceSetting, string newVoiceCommand)
        {
            var settingVoiceCommands = Get<List<string>>(voiceSetting);
            settingVoiceCommands.Add(newVoiceCommand);
            Set<List<string>>(voiceSetting, settingVoiceCommands);
        }

        public static void RemoveVoiceCommand(Settings voiceSetting, string voiceCommand)
        {
            var settingVoiceCommands = Get<List<string>>(voiceSetting);
            settingVoiceCommands = settingVoiceCommands.Where(vc => vc != voiceCommand).ToList();
            Set<List<string>>(voiceSetting, settingVoiceCommands);
        }
    }
}