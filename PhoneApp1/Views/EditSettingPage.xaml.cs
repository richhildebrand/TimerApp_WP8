using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
                }
            }
        }

        private bool IsValidCommand(string newCommand)
        {
            return (newCommand != null) && (newCommand.Trim().Length > 0);
        }
    }
}