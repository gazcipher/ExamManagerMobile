using System;
using System.Collections.Generic;
using System.Text;

namespace ExamManagerMobile.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string MatricNumber { get; set; }
        public Guid DepartmentID { get; set; }

        public RegisterModel()
        {

        }

        
    }
}
