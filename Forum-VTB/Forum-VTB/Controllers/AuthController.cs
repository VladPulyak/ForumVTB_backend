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

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserProfile>> Register(UserRegisterDto userRequestDto)
        {
            if (_userService.GetAll().FirstOrDefault(q => q.Login == userRequestDto.Login) is not null)
            {
                return BadRequest("User already exists");
            }
            var user = _authenticationService.MapUserProfile(userRequestDto);
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

            var token = _authenticationService.CreateAccessToken(user);
            var refreshToken = _authenticationService.GenerateRefreshToken();
            _authenticationService.SetRefreshToken(refreshToken, user);
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

            string token = _authenticationService.CreateAccessToken(userProfile);
            var newRefreshToken = _authenticationService.GenerateRefreshToken();
            _authenticationService.SetRefreshToken(newRefreshToken, userProfile);
            return Ok(token);
        }
    }
}