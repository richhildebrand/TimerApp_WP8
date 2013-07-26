using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace TimerUI.ViewModels
{
    public class SettingsPageViewModel
    {
        public string VoiceTimeout { get; set; }

        public SettingsPageViewModel()
        {
            //TODO: move to app load
            if (!IsolatedStorageSettings.ApplicationSettings.Contains("VoiceTimeout"))
            {
                IsolatedStorageSettings.ApplicationSettings.Add("VoiceTimeout", TimeSpan.FromMinutes(5));
            }
            VoiceTimeout = IsolatedStorageSettings.ApplicationSettings["VoiceTimeout"].ToString();
        }

        public void Cleanup()
        {
        }
    }
}
