using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using Telerik.Windows.Controls;

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

            var startCommands = new List<RecognizableString>();
            startCommands.Add(CreateRecognizableString("Start"));
            SetToDefaultIfUnset<List<RecognizableString>>("StartVoiceCommands", startCommands);

            var stopCommands = new List<RecognizableString>();
            stopCommands.Add(CreateRecognizableString("Stop"));
            SetToDefaultIfUnset<List<RecognizableString>>("StopVoiceCommands", stopCommands);
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

        private static RecognizableString CreateRecognizableString(string voiceCommand)
        {
            var recognizableString = new RecognizableString();
            recognizableString.Value = voiceCommand;
            return recognizableString;
        }

        public static void AddNewVoiceCommand(Settings voiceSetting, string newVoiceCommand)
        {
            var settingVoiceCommands = Get<List<RecognizableString>>(voiceSetting);
            settingVoiceCommands.Add(CreateRecognizableString(newVoiceCommand));
            Set<List<RecognizableString>>(voiceSetting, settingVoiceCommands);
        }
    }
}