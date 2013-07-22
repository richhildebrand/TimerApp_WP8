﻿using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using TimerUI.ViewModel;

namespace TimerUI.SpeechHandlers
{
    public class StartSpeechHandler : ISpeechInputHandler
    {
        private const string START_TIMER = "Start";
        private const string STOP_TIMER = "Stop";

        public bool CanHandleInput(string input)
        {
            return true;
        }

        public void HandleInput(FrameworkElement target, string input)
        {
            if (string.Compare(START_TIMER, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {

                var mainPageViewModel = new MainPageViewModel();
                var sender = new Object();
                mainPageViewModel.ToggleStartAndStopButton(sender);
                Telerik.Windows.Controls.SpeechManager.StartListening();

            }

            if (string.Compare(STOP_TIMER, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                //RadSlideView sv = target as RadSlideView;
                //sv.MoveToPreviousItem();
                Telerik.Windows.Controls.SpeechManager.StartListening();
            }
        }

        public void NotifyInputError(FrameworkElement target)
        {
            MessageBox.Show("Error");

        }
    }
}
