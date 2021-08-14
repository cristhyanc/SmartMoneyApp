using MvvmCross;
using SmartApp.App.Models;
using SmartApp.App.ViewModels.ExpiringThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartApp.App.Views.ExpiringThings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpiringThingsPage : ContentPage
    {
        ExpiringThingsViewModel _viewModel;
        public ExpiringThingsPage()
        {
            try
            {
                InitializeComponent();
                _viewModel = Mvx.IoCProvider.Resolve<ExpiringThingsViewModel>();
               
                BindingContext = _viewModel;
            }
            catch (Exception ex)
            {

                throw;
            }
         
            
        }

        private async void OnSignOut(object sender, EventArgs e)
        {
            var signout = await DisplayAlert("Sign out?", "Do you want to sign out?", "Yes", "No");
            if (signout)
            {
                await (Application.Current as App).SignOut();
            }
        }


        private void _viewModel_ScrollToEvent(object sender, ExpiringThingsModel e)
        {
            ItemsListView.ScrollTo(e, ScrollToPosition.End);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.ScrollToEvent += _viewModel_ScrollToEvent;
            _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.ScrollToEvent -= _viewModel_ScrollToEvent;

        }
    }
}