using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartApp.App.ViewModels
{
    public class UserAccountViewModel : BaseViewModel
    {
        public string UserId
        {
            get
            {
                return (Application.Current as App).UserID;
            }
        }

        public Command SignoutCommand { get; }

        public UserAccountViewModel()
        {
            SignoutCommand = new Command(async () => await Signout());
        }

        private async Task Signout()
        {
            try
            {
                await (Application.Current as App).SignOut();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
