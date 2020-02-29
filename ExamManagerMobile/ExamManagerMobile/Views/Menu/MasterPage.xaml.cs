using ExamManagerMobile.Models;
using ExamManagerMobile.Views.DetailViews;
using ExamManagerMobile.Views.DetailViews.SettingsViews;
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
	public partial class MasterPage : ContentPage
	{
        public ListView ListView { get { return listview;  } }
        public List<MasterMenuItem> items;
		public MasterPage()
		{
			InitializeComponent();
            SetItems();
		}

        void SetItems()
        {
            items = new List<MasterMenuItem>(); //initialized to prevent app from crashing
            items.Add(new MasterMenuItem("All Exams", "icon.png", Color.White, typeof(InfoScreenA)));
            items.Add(new MasterMenuItem("My Exams", "icon.png", Color.White, typeof(InfoScreenB)));
            items.Add(new MasterMenuItem("Settings", "icon.png", Color.White, typeof(SettingsScreen)));
            items.Add(new MasterMenuItem("Logout", "icon.png", Color.White, typeof(InfoScreenB)));

            ListView.ItemsSource = items;
        }


        void Logout(object sender, EventArgs e)
        {
            DisplayAlert("Login out so soon!", "Are you sure you want to Logout?", "Okay", "Cancel");
        } 


	}
}