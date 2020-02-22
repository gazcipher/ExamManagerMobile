using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExamManagerMobile.Data;
using Foundation;
using SQLite;
using UIKit;
using ExamManagerMobile.iOS.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_IOS))]
namespace ExamManagerMobile.iOS.Data
{
    public class SQLite_IOS : ISQLite
    {
        public SQLite_IOS()
        {

        }
        public SQLiteConnection GetDBConnection()
        {
            var fileName = "UserDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);

            var dbconn = new SQLite.SQLiteConnection(path);

            return dbconn;
        }
    }
}