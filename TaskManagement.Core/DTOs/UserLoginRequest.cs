using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;


namespace TaskManagement.Core.DTOs
{
    public class UserLoginRequest
    {
        //properties
        public string UserEmail { get; set; }
        public string Password { get; set; }

        //constructor
        public UserLoginRequest(string userEmail, string password) {
            UserEmail = userEmail;
            Password = password;
        }
    }
}
