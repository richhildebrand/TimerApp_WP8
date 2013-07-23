/*
  In App.xaml:
  <Application.Resources>
      <vm:GlobalViewModelLocator xmlns:vm="clr-namespace:WindowsPhoneApplication6.ViewModels"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

namespace TimerUI.ViewModel
{
    public class GlobalViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the GlobalViewModelLocator class.
        /// </summary>
        public GlobalViewModelLocator()
        {
            CreateMainPageViewModel();
            CreateTimeoutSettingsViewModel();
        }

        #region MainPageViewModel
        private static MainPageViewModel _mainPageViewModel;

        /// <summary>
        /// Gets the MainPageViewModel property.
        /// </summary>
        public static MainPageViewModel MainPageViewModelStatic
        {
            get
            {
                if (_mainPageViewModel == null)
                {
                    CreateMainPageViewModel();
                }

                return _mainPageViewModel;
            }
        }

        /// <summary>
        /// Gets the MainPageViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainPageViewModel MainPageViewModel
        {
            get
            {
                return MainPageViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the MainPageViewModel property.
        /// </summary>
        public static void ClearMainPageViewModel()
        {
            _mainPageViewModel.Cleanup();
            _mainPageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the MainPageViewModel property.
        /// </summary>
        public static void CreateMainPageViewModel()
        {
            if (_mainPageViewModel == null)
            {
                _mainPageViewModel = new MainPageViewModel();
            }
        }


        #endregion

        #region TimeoutSettingsViewModel
        private static TimeoutSettingsViewModel _timeoutSettingsViewModel;

        /// <summary>
        /// Gets the TimeoutSettingsViewModel property.
        /// </summary>
        public static TimeoutSettingsViewModel TimeoutSettingsViewModelStatic
        {
            get
            {
                if (_timeoutSettingsViewModel == null)
                {
                    CreateTimeoutSettingsViewModel();
                }

                return _timeoutSettingsViewModel;
            }
        }

        /// <summary>
        /// Gets the TimeoutSettingsViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public TimeoutSettingsViewModel TimeoutSettingsViewModel
        {
            get
            {
                return TimeoutSettingsViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the TimeoutSettingsViewModel property.
        /// </summary>
        public static void ClearTimeoutSettingsViewModel()
        {
            _timeoutSettingsViewModel.Cleanup();
            _timeoutSettingsViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the TimeoutSettingsViewModel property.
        /// </summary>
        public static void CreateTimeoutSettingsViewModel()
        {
            if (_timeoutSettingsViewModel == null)
            {
                _timeoutSettingsViewModel = new TimeoutSettingsViewModel();
            }
        }

        #endregion

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearMainPageViewModel();
            ClearTimeoutSettingsViewModel();
        }
    }
}