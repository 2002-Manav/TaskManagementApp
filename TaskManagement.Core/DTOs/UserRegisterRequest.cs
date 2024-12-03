using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain.Entities;

namespace TaskManagement.Core.DTOs
{
    public class UserRegisterRequest
    {
        //properties
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //constructor
        public UserRegisterRequest(string userName, string userEmail, string password, string role)
        {

            UserName = userName;
            UserEmail = userEmail;
            Password = password;
            Role = role;
        }

        //mapping
        public User ToUserEntity()
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                UserName = this.UserName,
                UserEmail = this.UserEmail,
                Password = this.Password,
                Role = this.Role
            };
        }
    }
}
