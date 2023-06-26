using AutoMapper;
using BusinessLayer.Dtos;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserManager<UserProfile> _userManager;
        public AuthService(IConfiguration configuration, IMapper mapper, UserManager<UserProfile> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
        }

        //public string CreateAccessToken(UserProfile userProfile)
        //{
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Email, userProfile.Email),
        //        new Claim(ClaimTypes.Role, userProfile.UserRole.Name)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        //    var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials, issuer: "Forum-VTB", audience: "Forum-VTB");
        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt;
        //}

        //public UserProfile MapUserProfile(UserRegisterDto userRequestDto)
        //{
        //    var user = new UserProfile();
        //    var hashPassword = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password);
        //    user.HashPassword = hashPassword;
        //    user.Login = userRequestDto.Login;
        //    user.Email = user.Login;
        //    user.NickName = "Unnamed";
        //    user.RoleId = 1;
        //    return user;
        //}

        //public RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken()
        //    {
        //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        DateOfCreating = DateTime.UtcNow,
        //        DateOfExpiring = DateTime.UtcNow.AddDays(7)
        //    };
        //    return refreshToken;
        //}

        //public void SetRefreshToken(RefreshToken refreshToken, UserProfile userProfile)
        //{
        //    var cookieOptions = new CookieOptions()
        //    {
        //        HttpOnly = true,
        //        Expires = refreshToken.DateOfExpiring
        //    };
        //    _contextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        //    userProfile.RefreshToken = refreshToken.Token;
        //    userProfile.DateOfCreating = refreshToken.DateOfCreating;
        //    userProfile.DateOfExpiring = refreshToken.DateOfExpiring;
        //    _userService.Update(userProfile);
        //    _userService.Save();
        //}


        public async Task<UserRegisterResponceDto> Register(UserRegisterDto registerUserDto)
        {
            List<IdentityError> resultErrors = new List<IdentityError>();
            var user = _mapper.Map<UserProfile>(registerUserDto);
            user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!addToRoleResult.Succeeded)
                {
                    resultErrors.AddRange(addToRoleResult.Errors);
                }
            }
            else
            {
                resultErrors.AddRange(result.Errors);
            }

            return new UserRegisterResponceDto
            {
                Errors = resultErrors,
                UserName = user.UserName,
                Email = user.Email
            };
        }
        public async Task<AuthResponceDto> Login(UserLoginDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Login);
            if (user is null)
            {
                throw new ObjectNotFoundException("User not found");
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (isValid)
            {
                var token = await GenerateToken(user);
                return new AuthResponceDto()
                {
                    UserEmail = user.Email,
                    Token = token,
                    RefreshToken = await CreateRefreshToken(user)
                };
            }
            else
            {
                throw new InvalidTokenException("Invalid login or password");
            }
        }
        public async Task<string> CreateRefreshToken(UserProfile user)
        {

            var loginProvider = "Forum-VTB.LoginProvider";
            var refreshToken = "Forum-VTB.RefreshToken";
            try
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, loginProvider, refreshToken);
                var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, loginProvider, refreshToken);
                var result = await _userManager.SetAuthenticationTokenAsync(user, loginProvider, refreshToken, newRefreshToken);
                if (result.Succeeded)
                {
                    return newRefreshToken;
                }
                else
                {
                    throw new InvalidTokenException("Invalid token!");
                }
            }
            catch (InvalidTokenException)
            {
                throw;
            }
        }

        public async Task<AuthResponceDto> VerifyRefreshToken(AuthResponceDto request)
        {
            var loginProvider = "Forum-VTB.LoginProvider";
            var myRefreshToken = "Forum-VTB.RefreshToken";
            try
            {
                var jwtSequrityTokenHandler = new JwtSecurityTokenHandler();
                if (!jwtSequrityTokenHandler.CanReadToken(request.Token))
                {
                    throw new InvalidTokenException("Invalid token!");
                }

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken();

                try
                {
                    var tokenContent = jwtSequrityTokenHandler.ReadJwtToken(request.Token);
                    var date = tokenContent.ValidTo;
                    jwtSecurityToken = tokenContent;
                }
                catch (InvalidTokenException)
                {
                    throw new InvalidTokenException("Invalid token!");
                }

                var userEmail = jwtSecurityToken.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
                var dateOfExpiring = jwtSecurityToken.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Exp)?.Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user is null || user.Email != request.UserEmail)
                {
                    throw new InvalidOperationException("User not found or email is incorrect");
                }

                var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(user, loginProvider, myRefreshToken, request.RefreshToken);
                if (isValidRefreshToken)
                {
                    var refreshToken = await CreateRefreshToken(user);
                    if (refreshToken is not null)
                    {
                        var token = await GenerateToken(user);
                        return new AuthResponceDto
                        {
                            Token = token,
                            UserEmail = user.Email,
                            RefreshToken = refreshToken,
                        };
                    }
                }

                await _userManager.UpdateSecurityStampAsync(user);
                throw new InvalidTokenException("Invalid token!");
            }
            catch (InvalidTokenException)
            {
                throw;
            }
        }

        private async Task<string> GenerateToken(UserProfile user)
        {
            var sequrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(sequrityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(20), signingCredentials: credentials, issuer: "Forum-VTB", audience: "Forum-VTB");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
