using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwimCheck.API.Models.DTOs._Auth_;
using SwimCheck.API.Repositories.Interfaces;

namespace SwimCheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _Auth_Controller : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public _Auth_Controller(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository) // UserManager class comes from Identity nugget package and DI at Startup.cs
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Register Functionality for users
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registed! Please login now.");
                    }
                }
            }

            return BadRequest("Something went wrong!");

        }

        // Login
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {

            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username); //find user by Email/Username

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password); //check if the user found matches the password

                if (checkPasswordResult)
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            jwtToken = jwtToken,
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password incorrect");

        }

    }
}
