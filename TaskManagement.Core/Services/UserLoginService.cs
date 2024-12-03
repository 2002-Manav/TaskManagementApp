using System.Security.Claims;
using System.Text;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TaskManagement.Core.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        // Constructor
        public UserLoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> LoginAsync(UserLoginRequest request)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(request.UserEmail) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Invalid login data.");

            // Fetch user by email
            var user = await _userRepository.GetUserByEmailAsync(request.UserEmail);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // Verify password
            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (passwordVerification != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // Create claims (including role)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.UserEmail)
            };

            if (!string.IsNullOrWhiteSpace(user.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role));  // Add role as claim
            }

            // Generate JWT token
            var token = GenerateJwtToken(claims);

            return token;
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            // Define your secret key, which should be kept safe and secure
            var secretKey = "Secret"; // Example, use a more secure key in production!

            // Create symmetric security key from the secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Create signing credentials
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define token expiration (e.g., 1 hour)
            var expiration = DateTime.UtcNow.AddHours(1);

            // Create the JWT token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Include claims (name, role, etc.)
                Expires = expiration, // Set expiration time for the token
                SigningCredentials = signingCredentials // Define signing credentials
            };

            // Initialize JwtSecurityTokenHandler to create the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string
            return tokenHandler.WriteToken(token);
        }
        
    }
}
