﻿using System;
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
        public SettingsManager.Settings SettingToModify { get; set; }


        public List<String> VoiceCommandList {
            get { return _voiceCommandList; }
            set { _voiceCommandList = value; NotifyOfPropertyChange(() => VoiceCommandList); }
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            UpdateVoiceCommandList();
        }

        public void AddNewCommand()
        {
            TextBlock messageTextBlock = new TextBlock();
            RadInputPrompt.Show(AddNewText,
                                MessageBoxButtons.OKCancel,
                                messageTextBlock,
                                closedHandler: (arg) => OnAddInputClose(arg)
            );
        }

        public void UpdateVoiceCommandList()
        {
            var voiceCommands = SettingsManager.Get<List<string>>(SettingToModify);
            VoiceCommandList = voiceCommands;
        }

        public void OnAddInputClose(InputPromptClosedEventArgs arg)
        {
            if (arg.Result == DialogResult.OK)
            {
                if (IsValidCommand(arg.Text))
                {
                    SettingsManager.AddNewVoiceCommand(SettingToModify, arg.Text.Trim());
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
