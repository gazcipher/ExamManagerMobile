using ExamManagerMobile.Data;
using ExamManagerMobile.Models;
using ExamManagerMobile.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExamManagerMobile
{
    public partial class App : Application
    {
        static TokenDBController _tokenDatabase;
        static UserDBController _userDatabase;
        static RestService restService; //Injecting the rest service into the app
        private static Label labelScreen;
        private static bool hasInternet;
        private static Page curentPage;

        //check internet every couple of seconds
        private static Timer timer;
        private static bool noInternetShow;

        public App()
        {
            InitializeComponent();

           // MainPage = new MainPage();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static UserDBController UserDatabase
        {
            get
            {
                if (_userDatabase == null)
                {
                    _userDatabase = new UserDBController();
                }
                return _userDatabase;
            }
        }
        public static TokenDBController TokenDatabase
        {
            get
            {
                if (_tokenDatabase == null)
                {
                    _tokenDatabase = new TokenDBController();
                }
                return _tokenDatabase;
            }
        }

        //=========================Making Internet Connection=================================
        public static RestService RestService
        {
            get
            {
                if(restService == null)
                {
                    restService = new RestService();
                }
                return restService;
            }
        }
        //=============================Ends internet Connection =============================

        //===========================Check if you can get internet===========================
        public static void StartCheckIfInternetConnection(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = true;
            curentPage = page;

            if(timer == null)
            {
                timer = new Timer((e) => {
                    StartCheckIfInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        private static void StartCheckIfInternetOverTime()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckInternetConnection();

            //check if internet is disabled, change UI element
            if(!networkConnection.IsConnected)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    if (hasInternet)
                    {
                        if (!noInternetShow)
                        {
                            hasInternet = false;
                            labelScreen.IsVisible = true;
                            await ShowDisplayAlert();
                        }
                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() => {
                    hasInternet = true;
                    labelScreen.IsVisible = false;
                });
            }
        }

        //This checks for internet connection Immediately i.e before you Press a button to process important transaction
        public static async Task<bool> CheckIfInternet()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckInternetConnection();
            return networkConnection.IsConnected;

        }

        //=======================Check internet with Alert==============================================================
        public static async Task<bool> CheckIfInternetWithAlertAsync()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckInternetConnection();
            if (!networkConnection.IsConnected)
            {
                if (!noInternetShow)
                {
                    await ShowDisplayAlert();
                }
                return false;
            }
            return true;
        }

        private static async Task ShowDisplayAlert()
        {
            noInternetShow = false;
            await curentPage.DisplayAlert("Device", "No internet Connection on your Device! Try to Reconnect", "Okay");
            noInternetShow = false;
        }


        //===========================Ends internet conn checking=======================================================

    }
}
