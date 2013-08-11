using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Caliburn.Micro;
using Telerik.Windows.Controls;
using TimerUI.AppInit;

namespace TimerUI.ViewModels
{
    public class EditSettingPageViewModel : Screen
    {
        private List<String> _voiceCommandList;
        public string AddNewText { get; set; }
        
        // Hack because I cannot get caliburn to pass the enum
        public string SettingToModifyHint { get; set; }
        public SettingsManager.Settings GetSettingToModify(string hint)
        {
            if (SettingsManager.Settings.StartVoiceCommands.ToString() == hint)
            {
                return SettingsManager.Settings.StartVoiceCommands;
            }
            else
            {
                return SettingsManager.Settings.StopVoiceCommands;
            }
        }

        public List<String> VoiceCommandList {
            get { return _voiceCommandList; }
            set { _voiceCommandList = value; NotifyOfPropertyChange(() => VoiceCommandList); }
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            var settingToModify = GetSettingToModify(SettingToModifyHint);
            UpdateVoiceCommandList(settingToModify);
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

        private void UpdateVoiceCommandList(SettingsManager.Settings settingToModify)
        {
            var voiceCommands = SettingsManager.Get<List<RecognizableString>>(settingToModify);
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
                    SettingsManager.Settings settingToModify = GetSettingToModify(SettingToModifyHint);
                    SettingsManager.AddNewVoiceCommand(settingToModify, arg.Text.Trim());
                    UpdateVoiceCommandList(settingToModify);
                }
            }
        }

        private bool IsValidCommand(string newCommand)
        {
            return (newCommand != null) && (newCommand.Trim().Length > 0);
        }
    }
}
