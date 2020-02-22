using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExamManagerMobile.Data;
using SQLite;
using Xamarin.Forms;
using ExamManagerMobile.Droid.Data;

[assembly: Dependency(typeof(SQLite_Android))]
namespace ExamManagerMobile.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {

        }
        public SQLiteConnection GetDBConnection()
        {
            var sqliteFileName = "UserDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);

            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}