using System;
using System.IO.IsolatedStorage;
using System.Linq;
using Caliburn.Micro;

namespace TimerUI.ViewModels
{
    public class SettingsPageViewModel : PropertyChangedBase
    {
        private int _voiceTimeout;

        public int MinVoiceTimeout { get { return 1; } }
        public int MaxVoiceTimeout { get { return 15; } }

        public int VoiceTimeout { 
            get { return _voiceTimeout; }
            set
            {
                _voiceTimeout = value;
                IsolatedStorageSettings.ApplicationSettings["VoiceTimeout"] = TimeSpan.FromMinutes(value);
                NotifyOfPropertyChange(() => VoiceTimeout);
            }
        }

        public SettingsPageViewModel()
        {
            var voiceTimeoutSetting = (TimeSpan)IsolatedStorageSettings.ApplicationSettings["VoiceTimeout"];
            _voiceTimeout = voiceTimeoutSetting.Minutes;
        }
    }
}
