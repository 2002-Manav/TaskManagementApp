using System.Security.Claims;
using System.Text;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserRepository _userRepository;

        // Constructor
        public UserLoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<string> LoginAsync(UserLoginRequest request)
        {
            // Validatation
            if (string.IsNullOrWhiteSpace(request.UserEmail) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Invalid login data.");

            // Fetch user by email
            var user = await _userRepository.GetUserByEmailAsync(request.UserEmail);
            if (user == null || user.Password != HashPassword(request.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");


            return user.Password;
        }

        //password hashing
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password)); 
        }

    }
}
