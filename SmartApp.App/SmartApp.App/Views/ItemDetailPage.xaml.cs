using SmartApp.App.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartApp.App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}