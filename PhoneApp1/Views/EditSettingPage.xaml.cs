using System;
using System.Linq;
using Telerik.Windows.Controls;
using TimerUI.AppInit;
using TimerUI.ViewModels;

namespace TimerUI.Views
{
    public partial class EditSettingPage
    {
        private string _commandToDelete;

        public EditSettingPage()
        {
            InitializeComponent();
            var x = new RadDataBoundListBox();
        }

        public void OnItemTap(object sender, ListBoxItemTapEventArgs e)
        {
            var targetListBox = sender as RadDataBoundListBox;
            _commandToDelete = targetListBox.SelectedValue.ToString();
            var message = "Are you sure you want to delete the voice command \"" + _commandToDelete + "\"";             

            RadMessageBox.Show(message, MessageBoxButtons.OKCancel, closedHandler: (arg) => DeleteSelectedsCommands(arg));
        }

        private void DeleteSelectedsCommands(MessageBoxClosedEventArgs  arg)
        {
            var vm = (EditSettingPageViewModel)this.DataContext;
            if (arg.Result == DialogResult.OK)
            {
                SettingsManager.RemoveVoiceCommand(vm.SettingToModify, this._commandToDelete);
                vm.UpdateVoiceCommandList();
            }
        }
    }
}