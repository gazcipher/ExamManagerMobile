﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamManagerMobile.Views.DetailViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoScreenA : ContentPage
	{
		public InfoScreenA ()
		{
			InitializeComponent();
            Init();
		}

        void Init()
        {
            ActivitySpinner.IsVisible = true;
        }
	}
}