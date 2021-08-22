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


using Microsoft.Identity.Client;
using Microsoft.Graph;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using TimeZoneConverter;
using Application = Xamarin.Forms.Application;
using SmartApp.App.Auth;
using System.ComponentModel;
using System.Threading.Tasks;
using SmartApp.App.ViewModels;

namespace SmartApp.App
{
    public partial class App : Application
    {
        // UIParent used by Android version of the app
        public static object AuthUIParent = null;

        // Keychain security group used by iOS version of the app
        private static string iOSKeychainSecurityGroup = null;

        // Microsoft Authentication client for native/mobile apps
        private static IPublicClientApplication PCA;

        // Microsoft Graph client
        private static GraphServiceClient GraphClient;

        // Microsoft Graph permissions used by app
        private readonly string[] Scopes = OAuthSettings.Scopes.Split(' ');
     

        private bool isSignedIn;
        public bool IsSignedIn
        {
            get { return isSignedIn; }
            set
            {
                isSignedIn = value;               
            }
        }

        public bool IsSignedOut { get { return !isSignedIn; } }

      
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
            }
        }

     
        private string userEmail;
        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
            }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
            }
        }
        

        private ImageSource userPhoto;
        public ImageSource UserPhoto
        {
            get { return userPhoto; }
            set
            {
                userPhoto = value;
                OnPropertyChanged("UserPhoto");
            }
        }
        
        
        public static TimeZoneInfo UserTimeZone;
        public App()
        {


            try
            {

                InitializeComponent();

                var builder = PublicClientApplicationBuilder
                            .Create(OAuthSettings.ApplicationId)
                            .WithBroker()
                            .WithRedirectUri(OAuthSettings.RedirectUri);

                if (!string.IsNullOrEmpty(iOSKeychainSecurityGroup))
                {
                    builder = builder.WithIosKeychainSecurityGroup(iOSKeychainSecurityGroup);
                }

                PCA = builder.Build();
               
                LoadCurrentUser().Wait();

                if(IsSignedIn)
                {                   
                    MainPage = new AppShell();
                }
                else
                {
                    MainPage = new LoginPage();
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private void RegisterDI()
        {
            DependencyService.Register<MockDataStore>();

            //AzureFuntionHttpClient httpClientFuntions = new AzureFuntionHttpClient("https://expiringthingsfc.azurewebsites.net/api");
            //httpClientFuntions.BaseAddress = new Uri("https://expiringthingsfc.azurewebsites.net/api");

            AzureFuntionHttpClient httpClientFuntions = new AzureFuntionHttpClient("http://192.168.1.100/api");
            httpClientFuntions.BaseAddress = new Uri("http://192.168.1.100/api");

            httpClientFuntions.DefaultRequestHeaders.Add("userID", UserID);
            MvxIoCProvider.Initialize();
            Mvx.IoCProvider.RegisterType<IExpiryngThingClient, ExpiryngThingClient>();
            Mvx.IoCProvider.RegisterSingleton<AzureFuntionHttpClient>(httpClientFuntions);
            Mvx.IoCProvider.RegisterType<ExpiringThingsViewModel, ExpiringThingsViewModel>();
            Mvx.IoCProvider.RegisterType<UserAccountViewModel, UserAccountViewModel>();            
        }

        private async Task LoadCurrentUser()
        {
            try
            {
                var accounts = await PCA.GetAccountsAsync();
                var silentAuthResult = await PCA.AcquireTokenSilent(Scopes, accounts.FirstOrDefault()).ExecuteAsync();
                Debug.WriteLine("User already signed in.");
                Debug.WriteLine($"Successful silent authentication for: {silentAuthResult.Account.Username}");
                Debug.WriteLine($"Access token: {silentAuthResult.AccessToken}");

                //  await InitializeGraphClientAsync();
                this.userID = silentAuthResult.Account.Username;

                 IsSignedIn = true;
                RegisterDI();

            }
            catch (MsalUiRequiredException msalEx)
            {
                IsSignedIn = false;
            }
            catch (Exception ex)
            {
                IsSignedIn = false;
            }
        }

        public async Task SignIn()
        {
            try
            {
                var accounts = await PCA.GetAccountsAsync();
                var silentAuthResult = await PCA.AcquireTokenSilent(Scopes, accounts.FirstOrDefault()).ExecuteAsync();
                Debug.WriteLine("User already signed in.");
                Debug.WriteLine($"Successful silent authentication for: {silentAuthResult.Account.Username}");
                Debug.WriteLine($"Access token: {silentAuthResult.AccessToken}");
                this.userID = silentAuthResult.Account.Username;
            }
            catch (MsalUiRequiredException msalEx)
            {
                Debug.WriteLine("Silent token request failed, user needs to sign-in: " + msalEx.Message);

                var interactiveRequest = PCA.AcquireTokenInteractive(Scopes);

                if(AuthUIParent!=null)
                {
                    interactiveRequest = interactiveRequest.WithParentActivityOrWindow(AuthUIParent);
                }

                var interactiveAuthResult = await interactiveRequest.ExecuteAsync();
                Debug.WriteLine($"Successful interactive authentication for: {interactiveAuthResult.Account.Username}");
                Debug.WriteLine($"Access token: {interactiveAuthResult.AccessToken}");
                this.userID = interactiveAuthResult.Account.Username;

            }
            catch (Exception ex)
            {
                throw;
            }
                       
           // await InitializeGraphClientAsync();
           
            IsSignedIn = true;
            RegisterDI();
            MainPage = new AppShell();

        }

        //private async Task InitializeGraphClientAsync()
        //{
        //    var currentAccounts = await PCA.GetAccountsAsync();
        //    try
        //    {
        //        if (currentAccounts.Count() > 0)
        //        {
        //            // Initialize Graph client
        //            GraphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
        //                async (requestMessage) =>
        //                {
        //                    var result = await PCA.AcquireTokenSilent(Scopes, currentAccounts.FirstOrDefault())
        //                        .ExecuteAsync();

        //                    requestMessage.Headers.Authorization =
        //                        new AuthenticationHeaderValue("Bearer", result.AccessToken);
        //                }));

        //            await GetUserInfo();

        //            IsSignedIn = true;
        //        }
        //        else
        //        {
        //            IsSignedIn = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Failed to initialized graph client.");
        //        Debug.WriteLine($"Accounts in the msal cache: {currentAccounts.Count()}.");
        //        Debug.WriteLine($"See exception message for details: {ex.Message}");
        //    }
        //}

        public async Task SignOut()
        {
            try
            {
                UserPhoto = null;
                UserName = string.Empty;
                UserEmail = string.Empty;
                UserID = string.Empty;
                IsSignedIn = false;

                var accounts = await PCA.GetAccountsAsync();
                while (accounts.Any())
                {
                    await PCA.RemoveAsync(accounts.First());
                    accounts = await PCA.GetAccountsAsync();
                }
                MainPage = new LoginPage();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private async Task GetUserInfo()
        //{
           
        //    try
        //    {
        //        var user = await GraphClient.Me.Request()
        //       .Select(u => new
        //       {
        //           u.DisplayName,
        //           u.Id,                   
        //           u.Mail
                 
        //       })
        //       .GetAsync();
              
        //        UserName = user.DisplayName;
        //        UserID = user.Id;
        //        UserEmail = string.IsNullOrEmpty(user.Mail) ? user.UserPrincipalName : user.Mail;
        //       // UserTimeZone = TZConvert.GetTimeZoneInfo(user.MailboxSettings.TimeZone);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
              
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
