using Microsoft.AspNetCore.Mvc;
using TestTask.DTOs;

namespace TestTask.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<ActionResult<UserDto>> Login(LoginDto loginDto);
        Task<bool> UserExists(string username);
    }
}
