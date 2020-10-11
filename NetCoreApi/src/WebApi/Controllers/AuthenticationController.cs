using System.Linq;
using System.Threading.Tasks;
using Audit.WebApi;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetCoreExampleAuth.Domain.Core.Model;
using NetCoreExampleAuth.Models.Common;
using NetCoreExampleAuth.Patterns.Configs;
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
            SignInManager<User> signInManager,
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
        public async Task<ActionResult<UserLoginResponseContract>> Authenticate([FromBody] UserForAuthenticationContract userForAuthContract)
        {
            User user = await this.userManager.FindByNameAsync(userForAuthContract.UserName);

            // no username in our base
            if (user is null)
            {
                return Unauthorized(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Wrong password or login."
                });
            }
            //userManager.lock
            var passCheck = await this.signInManager.CheckPasswordSignInAsync(user, userForAuthContract.Password, true);

            // Check password correct.
            if (!passCheck.Succeeded)
            {
                int leftAttempts = userManager.Options.Lockout.MaxFailedAccessAttempts - user.AccessFailedCount;
                return Unauthorized(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = passCheck.IsLockedOut ? $"The account is blocked to {user.LockoutEnd}" : $"Wrong password or login. {leftAttempts} login attempts remaining."
                });
            }

            var roles = this.userManager.GetRolesAsync(user);

            UserLoginResponseContract res = new UserLoginResponseContract()
            {
                UserName = user.UserName,
                UserId = user.Id.ToString(),
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
