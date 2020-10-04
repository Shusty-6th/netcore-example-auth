using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreExampleAuth.Entities.Models;
using NetCoreExampleAuth.Patterns.Contracts.Authentication;

namespace NetCoreExampleAuth.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IAuthenticationManager authManager;
        public AuthenticationController(
            IMapper mapper,
            UserManager<User> userManager,
            IAuthenticationManager authManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.authManager = authManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationContract user)
        {
            if (!await this.authManager.ValidateUser(user))
            {
                return Unauthorized();
            }
            return Ok(new { Token = await this.authManager.CreateToken() });
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
