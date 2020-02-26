using ExamManagerMobile.Models;
using ExamManagerMobile.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamManagerMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            lbl_Username.TextColor = Constants.MainTextColor;
            lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

            //check internet connection and display on label
            App.StartCheckIfInternetConnection(lbl_NoInternet, this); //check the current page we are on.

            entry_Username.Completed += (s, e) => entry_Password.Focus();
            entry_Password.Completed += (s, e) => LoginProcessing(s,e);
        }
        async void LoginProcessing(object sender, EventArgs e)
        {
            User user = new User(entry_Username.Text, entry_Password.Text);
            if(user.CheckIfValidCreds())
            {
                DisplayAlert("Login", "Login Succeeded", "Okay");
                //Login function should get back a token and the token can be saved using the token db controller
               // var result = await App.RestService.Login(user); //comment this out to test the dashboard
                var result = new Token(); //making a dummy token for testing the dashboard
                //if(result.access_token != null)
                if(result != null)
                {
                   // App.UserDatabase.CommitUser(user);
                   // App.TokenDatabase.CommitUser(result); //Navigation to dashboard after login

                    //await Navigation.PushAsync(new Dashboard());
                    if(Device.OS == TargetPlatform.Android)
                    {
                        //Overriding navigation stack
                        Application.Current.MainPage = new NavigationPage(new Dashboard());
                    }else if(Device.OS == TargetPlatform.iOS)
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new Dashboard()));
                    }
                }
            }
            else
            {
                DisplayAlert("Login", "Login Failed. Invalid Username or Password", "Okay");
            }

        }
    }
}