using BusinessLayer.Dtos;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Interfaces;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IUserService userService, IConfiguration configuration)
        {
            _authService = authService;
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserProfile>> Register(UserRegisterDto userRequestDto)
        {
            if (_userService.GetAll().FirstOrDefault(q => q.Login == userRequestDto.Login) is not null)
            {
                return BadRequest("User already exists");
            }
            var user = _authService.MapUserProfile(userRequestDto);
            await _userService.Add(user);
            await _userService.Save();
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
        {
            UserProfile user;

            try
            {
                user = await _userService.GetByLogin(userLoginDto.Login);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.HashPassword))
            {
                return BadRequest("Wrong password!");
            }

            var token = _authService.CreateAccessToken(user);
            var refreshToken = _authService.GenerateRefreshToken();
            _authService.SetRefreshToken(refreshToken, user);
            return Ok(token);
        }

        [HttpPost("RefreshToken")]
        [Authorize]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var userEmail = User.Claims.ToArray()[0].Value;
            UserProfile? userProfile;
            try
            {
                userProfile = await _userService.GetByLogin(userEmail);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User not found");
            }
            if (!userProfile.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh token!");
            }
            else if (userProfile.DateOfExpiring < DateTime.UtcNow)
            {
                return Unauthorized("Token Expired!");
            }

            string token = _authService.CreateAccessToken(userProfile);
            var newRefreshToken = _authService.GenerateRefreshToken();
            _authService.SetRefreshToken(newRefreshToken, userProfile);
            return Ok(token);
        }

        [HttpPost("Google")]
        [Authorize]
        public ActionResult GetUsers()
        {
            return Ok(_userService.GetAll());
        }

        private string CreateAccessToken(UserProfile userProfile)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userProfile.Email),
                new Claim(ClaimTypes.Role, userProfile.UserRole.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}