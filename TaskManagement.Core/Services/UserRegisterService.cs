using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public class UserRegisterService :  IUserRegisterService
    {
        private IUserRepository _userRepository;

        public UserRegisterService(IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

   

        //new
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UserRegisterAsync(UserRegisterRequest request)
        {
            // validation of user
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Invalid user data.");

            //checking if user exists
            var existingUser = await _userRepository.GetUserByEmailAsync(request.UserEmail);

            // hashing the password
            var hashedPassword = HashPassword(request.Password);

   
            //new user
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                UserEmail = request.UserEmail,
                Password = hashedPassword,
                Role = request.Role
            };

            //save in db
            return await _userRepository.AddUserAsync(newUser);
        }

        private string HashPassword(string password)
        {
            
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}
