using System;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Core.Services
{
    public class UserRegisterService : IUserRegisterService
    {
        private IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        // Inject PasswordHasher and UserRepository
        public UserRegisterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UserRegisterAsync(UserRegisterRequest request)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Invalid user data.");

            // Check if user already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(request.UserEmail);
            if (existingUser != null)
                throw new ArgumentException("User already exists.");

            // Hash the password
            var hashedPassword = _passwordHasher.HashPassword(null, request.Password);

            // Create new user
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                UserEmail = request.UserEmail,
                Password = hashedPassword,
                Role = request.Role 
            };

            // Save user to the database
            return await _userRepository.AddUserAsync(newUser);
        }
    }
}
