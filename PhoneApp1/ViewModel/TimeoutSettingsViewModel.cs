using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerUI.ViewModel
{
    public class TimeoutSettingsViewModel
    {
        public string VoiceTimeout { get; set; }

        public TimeoutSettingsViewModel()
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
