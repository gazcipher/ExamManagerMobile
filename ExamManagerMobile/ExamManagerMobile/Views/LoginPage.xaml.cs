using ExamManagerMobile.Models;
using ExamManagerMobile.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            //lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

            //check internet connection and display on label
            App.StartCheckIfInternetConnection(lbl_NoInternet, this); //check the current page we are on.

            entry_Username.Completed += (s, e) => entry_Password.Focus();
            entry_Password.Completed += (s, e) => LoginProcessing(s,e);
        }
        async void LoginProcessing(object sender, EventArgs e)
        {
            //email validation
            var loginEmail = entry_Username.Text;
        
            User user = new User(entry_Username.Text, entry_Password.Text);


            if(user.CheckIfValidCreds())
            {
                //Activity Spinner
                ActivitySpinner.IsVisible = true;
                //Login function should get back a token and the token can be saved using the token db controller
                //var result = await App.RestService.Login(user); //comment this out to test the dashboard

                if (!Regex.IsMatch(loginEmail, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                               RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    ErrorLabel.Text = "Invalid Email Address, Please check your email!";
                    return;
                }

                var result = new Token(); //making a dummy token for testing the dashboard

                await DisplayAlert("Login", "Login Succeeded", "Okay");

                if(App.SettingsDatabase.GetSettings() == null)
                {
                    Settings settings = new Models.Settings();
                    App.SettingsDatabase.SaveSettings(settings);
                }

                //if(result.access_token != null)
                if(result != null)
                {
                    ActivitySpinner.IsVisible = false;
                    // App.UserDatabase.CommitUser(user);
                    // App.TokenDatabase.CommitUser(result); //Navigation to dashboard after login

                    //await Navigation.PushAsync(new Dashboard());
                    if (Device.OS == TargetPlatform.Android)
                    {
                        //Overriding navigation stack
                        Application.Current.MainPage = new NavigationPage(new MasterDetail());

                    }else if(Device.OS == TargetPlatform.iOS)
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new MasterDetail()));
                    }
                }
            }
            else
            {
                await DisplayAlert("Login", "Login Failed. Invalid Username or Password", "Okay");
                ActivitySpinner.IsVisible = false;
            }
        }

        void regiser_page_btn(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new RegisterPage());
        }





    }
}