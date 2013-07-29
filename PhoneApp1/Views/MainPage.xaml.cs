using System;
using System.Linq;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace TimerUI.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Telerik.Windows.Controls.SpeechManager.StartListening();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Telerik.Windows.Controls.SpeechManager.Reset();
            base.OnNavigatedFrom(e);
        }
    }
}