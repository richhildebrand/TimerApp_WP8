using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Caliburn.Micro;
using Telerik.Windows.Controls;
using TimerUI.AppInit;

namespace TimerUI.ViewModels
{
    public class EditSettingPageViewModel : PropertyChangedBase
    {
        private List<String> _voiceCommandList;

        public List<String> VoiceCommandList {
            get { return _voiceCommandList; }
            set { _voiceCommandList = value; NotifyOfPropertyChange(() => VoiceCommandList); }
        }

        public EditSettingPageViewModel()
        {
            UpdateVoiceCommandList();
        }

        public void AddNewStopCommand()
        {
            TextBlock messageTextBlock = new TextBlock();
            RadInputPrompt.Show("Add a new stop command",
                                MessageBoxButtons.OKCancel,
                                messageTextBlock,
                                closedHandler: (arg) => OnStopInputClose(arg)
            );
        }

        private void UpdateVoiceCommandList()
        {
            var voiceCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StopVoiceCommands);
            var stringListOfVoiceCommands = new List<string>();
            voiceCommands.ForEach(vc => stringListOfVoiceCommands.Add(vc.Value));
            VoiceCommandList = stringListOfVoiceCommands;
        }

        public void OnStopInputClose(InputPromptClosedEventArgs arg)
        {
            if (arg.Result == DialogResult.OK)
            {
                if (IsValidCommand(arg.Text))
                {
                    SettingsManager.AddNewVoiceCommand(SettingsManager.Settings.StopVoiceCommands, arg.Text.Trim());
                    UpdateVoiceCommandList();
                }
            }
        }

        private bool IsValidCommand(string newCommand)
        {
            return (newCommand != null) && (newCommand.Trim().Length > 0);
        }
    }
}
