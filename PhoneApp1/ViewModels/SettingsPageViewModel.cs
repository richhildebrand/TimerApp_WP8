using System;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using TimerUI.AppInit;

namespace TimerUI.ViewModels
{
    public class SettingsPageViewModel : PropertyChangedBase
    {
        private int _voiceTimeout;
        private string _addMenuItemText;
        private Uri _icon;
        private readonly INavigationService _navigationService;

        public int MinVoiceTimeout { get { return 1; } }
        public int MaxVoiceTimeout { get { return 15; } }

        public SettingsPageViewModel(INavigationService navigationService)
        {
            var voiceTimeoutSetting = SettingsManager.Get<TimeSpan>(SettingsManager.Settings.VoiceTimeout);
            _voiceTimeout = voiceTimeoutSetting.Minutes;
            _navigationService = navigationService;
            AddMenuItemText = "Timer";
            Icon = new Uri("/Images/appbar.settings.png", UriKind.Relative);
        }

        public string AddMenuItemText
        {
            get { return _addMenuItemText; }
            set { _addMenuItemText = value; NotifyOfPropertyChange(() => AddMenuItemText); }
        }

        public Uri Icon
        {
            get { return _icon; }
            set { _icon = value; NotifyOfPropertyChange(() => Icon); }
        }

        public void NavigateToMainPage()
        {
            _navigationService.UriFor<MainPageViewModel>().Navigate();
        }

        public int VoiceTimeout { 
            get { return _voiceTimeout; }
            set
            {
                _voiceTimeout = value;
                SettingsManager.Set<TimeSpan>(SettingsManager.Settings.VoiceTimeout, TimeSpan.FromMinutes(value));
                NotifyOfPropertyChange(() => VoiceTimeout);
            }
        }
    }
}
