using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TestTask.Data;
using TestTask.DTOs;
using TestTask.Exceptions;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly TaskDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public AuthorizationService(TaskDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var admin = await _dbContext.Users
                .SingleOrDefaultAsync(a => a.Username == loginDto.Username.ToLower());

            if (admin == null)
                throw new UnauthorizedException("Invalid Username!");

            using var hmac = new HMACSHA512(admin.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != admin.PasswordHash[i])
                    throw new UnauthorizedException("Invalid Password!");
            }

            return new UserDto
            {
                Username = admin.Username,
                Token = _tokenService.CreateToken(admin)
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();

            var admin = new User
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _dbContext.Users.AddAsync(admin);
            await _dbContext.SaveChangesAsync();

            return new UserDto
            {
                Username = admin.Username,
                Token = _tokenService.CreateToken(admin)
            };
        }


        public async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
