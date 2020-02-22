using ExamManagerMobile.Data;
using ExamManagerMobile.Views;
using System;
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

        //=========================Making Internet Connectin=================================
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

    }
}
