using ExamManagerMobile.Models;
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

            entry_Username.Completed += (s, e) => entry_Password.Focus();
            entry_Password.Completed += (s, e) => LoginProcessing(s,e);
        }
        void LoginProcessing(object sender, EventArgs e)
        {
            User user = new User(entry_Username.Text, entry_Password.Text);
            if(user.CheckIfValidCreds())
            {
                DisplayAlert("Login", "Login Succeeded", "Okay");
                App.UserDatabase.CommitUser(user);
            }
            else
            {
                DisplayAlert("Login", "Login Failed. Invalid Username or Password", "Okay");
            }

        }
    }
}