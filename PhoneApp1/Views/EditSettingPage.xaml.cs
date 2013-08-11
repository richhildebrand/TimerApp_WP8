using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using TimerUI.AppInit;

namespace TimerUI.Views
{
    public partial class EditSettingPage : PhoneApplicationPage
    {
        public EditSettingPage()
        {
            InitializeComponent();
            UpdateVoiceCommandList();
        }

        private void UpdateVoiceCommandList()
        {
            var voiceCommands = SettingsManager.Get<List<RecognizableString>>(SettingsManager.Settings.StopVoiceCommands);
            var stringListOfVoiceCommands = new List<string>();
            voiceCommands.ForEach(vc => stringListOfVoiceCommands.Add(vc.Value));
            VoiceCommandList.ItemsSource = stringListOfVoiceCommands;
        }

        private void AddNewStopCommand(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock messageTextBlock = new TextBlock();
            RadInputPrompt.Show("Add a new stop command", 
                                MessageBoxButtons.OKCancel, 
                                messageTextBlock,
                                closedHandler: (arg) => OnStopInputClose(arg)
            );
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