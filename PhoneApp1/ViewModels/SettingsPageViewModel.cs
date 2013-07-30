using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace TimerUI.ViewModels
{
    public class SettingsPageViewModel
    {
        private int _voiceTimeout;

        public int VoiceTimeout { 
            get { return _voiceTimeout; }
            set
            {
                _voiceTimeout = value;
                IsolatedStorageSettings.ApplicationSettings["VoiceTimeout"] = TimeSpan.FromMinutes(value);
            }
        }

        public SettingsPageViewModel()
        {
            var voiceTimeoutSetting = (TimeSpan)IsolatedStorageSettings.ApplicationSettings["VoiceTimeout"];
            _voiceTimeout = voiceTimeoutSetting.Minutes;
        }
    }
}
