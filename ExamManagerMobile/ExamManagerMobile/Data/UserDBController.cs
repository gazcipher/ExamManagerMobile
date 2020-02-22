using ExamManagerMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamManagerMobile.Data
{
    public class UserDBController
    {
        static object locker = new object();

        //this create a table in the database
        SQLiteConnection database;

        public UserDBController()
        {
            //if table already exist then skip
            database = DependencyService.Get<ISQLite>().GetDBConnection();
            database.CreateTable<User>();
        }

        public User GetUser()
        {
            lock (locker)
            {
                if(database.Table<User>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<User>().First();
                }
            }
        }

        //==============================Save user data to db================================
        public int CommitUser(User user)
        {
            lock (locker)
            {
                if(user.ID != 0)
                {
                    database.Update(user); //update exisiting user
                    return user.ID;
                }
                else
                {
                    return database.Insert(user); //create new user
                }
            }
        }


        //=============================Delete user============================================
        public int RemoveUser(int id)
        {
            lock (locker)
            {
                return database.Delete<User>(id);
            }
        }
    }
}
