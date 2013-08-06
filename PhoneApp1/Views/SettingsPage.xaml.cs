using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using TimerUI.AppInit;

namespace TimerUI.Views
{
    public partial class SettingsPage
    {
        public string NewStartVoiceCommand { get; set; }

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void LostFocusNewStartVoiceCommand(object sender, RoutedEventArgs e)
        {
            SaveNewVoiceCommand(SettingsManager.Settings.StartVoiceCommands, sender as RadTextBox);
        }

        private void ActionButtonSaveNewStartVoiceCommand(object sender, EventArgs e)
        {
            SaveNewVoiceCommand(SettingsManager.Settings.StartVoiceCommands, sender as RadTextBox);
        }

        private void SaveNewVoiceCommand(SettingsManager.Settings setting, RadTextBox radTextBox)
        {
            SettingsManager.AddNewVoiceCommand(setting, radTextBox.Text);
        }
    }
}
