using Microsoft.AspNetCore.Mvc;
using TestTask.DTOs;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAuthorizationService _authorizationService;

        public AccountController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _authorizationService.UserExists(registerDto.Username))
                return BadRequest("Username is taken!");

            return Ok(await _authorizationService.Register(registerDto));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return Ok(await _authorizationService.Login(loginDto));
        }
    }
}
