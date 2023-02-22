using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        public DataContext _context;
        public ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        //now that I have a service to create my token I inject it here
        //BUT notice that it is the INTERFACE not the actual implementation I wrote
        //the framework's job, based on what I specified in my service container (program.cs)...
        //to look at what implementation is being used for the token service
        {
            _context = context;
            _tokenService = tokenService;

        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("User already exists!");
            }
            //need to take the plaintext password and hash it using a hashing algorithm
            //.net framework provides classes to do this ^ 
            //I used 'using' below so that this will be disposed of after use and not take up space in memory
            //^ i can do this becasue this particular class derives from a class that derives from IDisposable
            //^ IDisposable has a dispose() method that gets called for me if I put the using keyword here
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                //^ all this extra stuff is to get the byte array
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.UserName);
            if (user == null) return Unauthorized("No user exists with this username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedPasswordHash.Length; i++)
            {
                if (computedPasswordHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }
    }
}