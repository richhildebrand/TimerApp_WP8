using System;
using System.Linq;
using Caliburn.Micro;
using TimerUI.AppInit;

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
                SettingsManager.Set<TimeSpan>(SettingsManager.Settings.VoiceTimeout, TimeSpan.FromMinutes(value));
                NotifyOfPropertyChange(() => VoiceTimeout);
            }
        }

        public SettingsPageViewModel()
        {
            var voiceTimeoutSetting = SettingsManager.Get<TimeSpan>(SettingsManager.Settings.VoiceTimeout);
            _voiceTimeout = voiceTimeoutSetting.Minutes;
        }
    }
}
