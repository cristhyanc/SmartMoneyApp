using MvvmCross;
using MvvmCross.IoC;
using SmartApp.App.Services;
using SmartApp.App.ViewModels.ExpiringThings;
using SmartApp.App.Views;
using SmartApp.Client.BL.Api;
using SmartApp.Client.BL.ExpiryngThing;
using SmartApp.Common.Interfaces.Client;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartApp.App
{
    public partial class App : Application
    {

        public App()
        {
           

            try
            {
                InitializeComponent();
                DependencyService.Register<MockDataStore>();
                // DependencyService.RegisterSingleton<AzureFuntionHttpClient>(httpClientFuntions);
                // DependencyService.Register<IExpiryngThingClient, ExpiryngThingClient>();



                AzureFuntionHttpClient httpClientFuntions = new AzureFuntionHttpClient("http://192.168.1.100/api");
                httpClientFuntions.BaseAddress = new Uri("http://192.168.1.100/api");
                MvxIoCProvider.Initialize();
                Mvx.IoCProvider.RegisterType<IExpiryngThingClient, ExpiryngThingClient>();
                Mvx.IoCProvider.RegisterSingleton<AzureFuntionHttpClient>(httpClientFuntions);
                Mvx.IoCProvider.RegisterType<ExpiringThingsViewModel, ExpiringThingsViewModel>();

                MainPage = new AppShell();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        
    }
}
