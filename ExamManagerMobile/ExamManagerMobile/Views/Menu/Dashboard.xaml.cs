using ExamManagerMobile.Models;
using ExamManagerMobile.Views.DetailViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamManagerMobile.Views.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dashboard : ContentPage
	{
		public Dashboard ()
		{
			InitializeComponent ();
            Init();
		}

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            //check internet connection and display on label
            App.StartCheckIfInternetConnection(lbl_NoInternet, this); //check the current page we are on.
        }

        async void SelectedScreenA(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoScreenA()); //Move to the next screen
        }

    }
}