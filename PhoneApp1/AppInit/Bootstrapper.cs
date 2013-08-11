using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using TimerUI.ViewModels;

namespace TimerUI.AppInit
{
    public class Bootstrapper : PhoneBootstrapper
    {
        public PhoneContainer Container { get; set; }

        protected override void Configure()
        {
            Container = new PhoneContainer();
            SettingsManager.ApplyDefaultsToAnyUnsetValues();

            Container.RegisterPhoneServices(RootFrame);

            Container.PerRequest<MainPageViewModel>();
            Container.PerRequest<SettingsPageViewModel>();
            Container.PerRequest<EditSettingPageViewModel>();

            ConventionManager.AddElementConvention<BindableAppBarButton>(
            Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
            Control.IsEnabledProperty, "DataContext", "Click");
            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            //ellided  
        }

        protected override object GetInstance(Type service, string key)
        {
            return Container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }
    }
}