using MvvmCross;
using SmartApp.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartApp.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccountPage : ContentPage
    {
        UserAccountViewModel _viewModel;
        public UserAccountPage()
        {
            try
            {
                InitializeComponent();
                _viewModel = Mvx.IoCProvider.Resolve<UserAccountViewModel>();
                BindingContext = _viewModel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
     
    }
}