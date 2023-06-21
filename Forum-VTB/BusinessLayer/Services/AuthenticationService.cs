using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthenticationService(IConfiguration configuration, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public string CreateAccessToken(UserProfile userProfile)
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

        public UserProfile MapUserProfile(UserRegisterDto userRequestDto)
        {
            var user = new UserProfile();
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password);
            user.HashPassword = hashPassword;
            user.Login = userRequestDto.Login;
            user.Email = user.Login;
            user.NickName = "Unnamed";
            user.RoleId = 1;
            return user;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                DateOfCreating = DateTime.UtcNow,
                DateOfExpiring = DateTime.UtcNow.AddDays(7)
            };
            return refreshToken;
        }

        public void SetRefreshToken(RefreshToken refreshToken, UserProfile userProfile)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = refreshToken.DateOfExpiring
            };
            _contextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            userProfile.RefreshToken = refreshToken.Token;
            userProfile.DateOfCreating = refreshToken.DateOfCreating;
            userProfile.DateOfExpiring = refreshToken.DateOfExpiring;
            _userService.Update(userProfile);
            _userService.Save();
        }

    }
}
