using ExamManagerMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamManagerMobile.Data
{
    public class TokenDBController
    {
        static object locker = new object();

        //this create a table in the database
        SQLiteConnection database;

        public TokenDBController()
        {
            //if table already exist then skip
            database = DependencyService.Get<ISQLite>().GetDBConnection();
            database.CreateTable<Token>();
        }

        public Token GetToken()
        {
            lock (locker)
            {
                if (database.Table<Token>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Token>().First();
                }
            }
        }

        //==============================Save user data to db================================
        public int CommitUser(Token token)
        {
            lock (locker)
            {
                if (token.ID != 0)
                {
                    database.Update(token); //update exisiting user
                    return token.ID;
                }
                else
                {
                    return database.Insert(token); //create new user
                }
            }
        }


        //=============================Delete user============================================
        public int RemoveUser(int id)
        {
            lock (locker)
            {
                return database.Delete<Token>(id);
            }
        }
    }
}
