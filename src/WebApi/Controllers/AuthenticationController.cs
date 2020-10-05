using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreExampleAuth.Entities.Models;
using NetCoreExampleAuth.Patterns.Contracts.Authentication;

namespace NetCoreExampleAuth.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;
        private readonly IAuthenticationManager authManager;
        public AuthenticationController(
            IMapper mapper,
            UserManager<User> userManager,
            //SignInManager<User> signInManager,
            IAuthenticationManager authManager
            )
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authManager = authManager;
        }

        //[HttpGet("user")]
        ////[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> Authenticate()
        //{
        //    await this.userManager.Users.ToList().Select(s => s.claim)

        //    return Ok();
        //}

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userForAuthContract">username and password</param>
        /// <returns>info about logged user with token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationContract userForAuthContract)
        {
            //var res = await this.signInManager.PasswordSignInAsync(user.UserName, user.Password, false, true);

            var user = await this.userManager.FindByNameAsync(userForAuthContract.UserName);

            // Check if the user is in the database and password is correct.
            if (!(user != null && await this.userManager.CheckPasswordAsync(user, userForAuthContract.Password)))
            {
                return Unauthorized();
            }
            var roles = this.userManager.GetRolesAsync(user);

            UserLoginResponseContract res = new UserLoginResponseContract()
            {
                UserId = user.Id,
                UserFullName = $"{user.FirstName} {user.LastName}",
                Token = await this.authManager.CreateToken(user),
                Roles = (await roles).ToArray()
            };

            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationContract userForRegistration)
        {
            var user = mapper.Map<User>(userForRegistration);

            var result = await userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
