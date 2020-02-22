using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamManagerMobile.Models
{
    public class User
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string UserName { get; set; }
        //public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }

        //Check input values
        public bool CheckIfValidCreds()
        {
            if(!this.UserName.Equals("") && !this.Password.Equals(""))
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}
