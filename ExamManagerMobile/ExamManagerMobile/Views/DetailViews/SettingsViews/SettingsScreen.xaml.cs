using ExamManagerMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamManagerMobile.Views.DetailViews.SettingsViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsScreen : ContentPage
	{
        Settings settings;
        SwitchCell switchCell1;
        SwitchCell switchCell2;
        // User currentUser;


        public SettingsScreen ()
		{
			InitializeComponent ();
            BackgroundColor = Constants.BackgroundColor;
            loadSettings();
            App.StartCheckIfInternetConnection(lbl_NoInternet, this);
            Title = Constants.SettingsScreenTitle;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.StartCheckIfInternetConnection(lbl_NoInternet, this);
        }

        private void loadSettings()
        {
            settings = App.SettingsDatabase.GetSettings();
            //currentUser = App.UserDatabase.GetUser();

            TableView table;

            switchCell1 = new SwitchCell
            {
                Text = "SwitchCell 1",
                //IsEnabled = settings.Switch1
                On = settings.Switch1
            };
            switchCell1.OnChanged += (object sender, ToggledEventArgs e) =>
            {
                SwitchCell1Switched(sender, e);
            };


            switchCell2 = new SwitchCell
            {
                Text = "SwitchCell 2",
                On = settings.Switch2
            };
            switchCell2.OnChanged += (object sender, ToggledEventArgs e) =>
            {
                SwitchCell2Switched(sender, e);
            };

            table = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection
                    {
                        switchCell1,
                        switchCell2
                    }
                }
            };

            table.VerticalOptions = LayoutOptions.FillAndExpand;

            MainLayout.Children.Add(table);
        }

        private void SwitchCell2Switched(object sender, ToggledEventArgs e)
        {
            settings.Switch2 = e.Value;
        }

        private void SwitchCell1Switched(object sender, ToggledEventArgs e)
        {
            settings.Switch1 = e.Value;
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var action = await DisplayAlert("Settings", "Are you sure you want to save settings?", "Okay", "Cancel");
            if(action)
                SaveSettings();
        }

        private void SaveSettings()
        {
            App.SettingsDatabase.SaveSettings(settings);
        }







    }
}