using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Domain.Identities;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;
        private readonly IUserRegisterService _userRegisterService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Injecting services into the controller
        public UserAuthController(IUserLoginService userLoginService, IUserRegisterService userRegisterService,
                                  UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userLoginService = userLoginService;
            _userRegisterService = userRegisterService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register API
        /// <summary>
        /// User Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest request)
        {
            if (request == null)
                return BadRequest("Invalid register request.");

            // Validate the request, create the user
            try
            {
                var user = new ApplicationUser
                {
                    UserName = request.UserName,
                    Email = request.UserEmail,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(new { Message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Login API
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            // unauth.
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Checking password
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Login successful!" });
            }

           //return invalid auth.
            return Unauthorized("Invalid email or password.");
        }


    }
}
