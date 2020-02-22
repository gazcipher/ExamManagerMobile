using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamManagerMobile.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        public static Color BackgroundColor = Color.FromRgb(58, 153, 215);
        public static Color MainTextColor = Color.White;
        public static int LoginIconHeight = 120;

        //=======================Login URL=================================
        public static string Login_Url = "https://exam-manager-api.azurewebsites.net/api/v1/auth/signin";
    }
}
