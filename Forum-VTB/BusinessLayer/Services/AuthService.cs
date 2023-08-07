using AutoMapper;
using BusinessLayer.Dtos.Authentication;
using BusinessLayer.Dtos.UserThemes;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Telegram.Bot.Types;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserThemeService _userThemeService;
        private readonly IMapper _mapper;
        private readonly UserManager<UserProfile> _userManager;
        public AuthService(IConfiguration configuration, IMapper mapper, UserManager<UserProfile> userManager, IUserThemeService userThemeService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            _userThemeService = userThemeService;
        }

        public async Task<UserRegisterResponceDto> Register(UserRegisterDto registerUserDto)
        {
            UserProfile user;

            if (await _userManager.FindByEmailAsync(registerUserDto.Email) is not null)
            {
                throw new DuplicateUserException("User with this email already exists");
            }
            user = _mapper.Map<UserProfile>(registerUserDto);
            user.UserName = registerUserDto.Email;
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            try
            {
                if (result.Succeeded)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (addToRoleResult.Succeeded)
                    {
                        user.Theme = await _userThemeService.AddUserTheme(new AddUserThemeDto
                        {
                            Theme = "dark",
                            UserId = user.Id
                        });
                        var authResponceDto = await Login(new UserLoginDto
                        {
                            Login = registerUserDto.Email,
                            Password = registerUserDto.Password
                        });
                        return new UserRegisterResponceDto
                        {
                            Token = authResponceDto.Token,
                            UserEmail = authResponceDto.UserEmail,
                            RefreshToken = authResponceDto.RefreshToken,
                            Theme = user.Theme.Theme
                        };
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        throw new InvalidOperationException(addToRoleResult.Errors.First().Description);
                    }
                }
                else
                {
                    throw new InvalidOperationException(result.Errors.First().Description);
                }

            }
            catch (Exception)
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
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
                    Theme = _userThemeService.GetByUserId(user.Id).Result.Theme,
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
                    throw new InvalidTokenException($"{result.Errors.First().Description}");
                }
            }
            catch (InvalidTokenException)
            {
                throw;
            }
        }

        public async Task<AuthResponceDto> RefreshToken(AuthResponceDto request)
        {
            var loginProvider = "Forum-VTB.LoginProvider";
            var myRefreshToken = "Forum-VTB.RefreshToken";
            var jwtSequrityTokenHandler = new JwtSecurityTokenHandler();
            if (!jwtSequrityTokenHandler.CanReadToken(request.Token))
            {
                throw new InvalidTokenException("Invalid token!");
            }

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken();

            try
            {
                var tokenContent = jwtSequrityTokenHandler.ReadJwtToken(request.Token);
                jwtSecurityToken = tokenContent;
            }
            catch (InvalidTokenException)
            {
                throw new InvalidTokenException("Token could not be read!");
            }

            var userEmail = jwtSecurityToken.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null || user.Email != request.UserEmail)
            {
                throw new ObjectNotFoundException("User with this email is not found");
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
            throw new InvalidTokenException("Invalid refresh token!");
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
                new Claim(JwtRegisteredClaimNames.NameId, user.Id)
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(20), signingCredentials: credentials, issuer: "Forum-VTB", audience: "Forum-VTB");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponceDto> GoogleAuthentication(GoogleAuthRequestDto requestDto)
        {
            var jwtSecurityToken = ConvertOAuthTokenToJWT(requestDto.OAuthToken);
            var userEmail = jwtSecurityToken.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;

            AuthResponceDto responce;

            var searchedUser = await _userManager.FindByEmailAsync(userEmail);
            if (searchedUser is null)
            {
                var user = new UserProfile()
                {
                    Email = userEmail,
                    EmailConfirmed = true,
                    NormalizedEmail = userEmail.ToUpper(),
                    UserName = userEmail,
                    NormalizedUserName = userEmail.ToUpper()
                };
                var errors = await RegisterByGoogle(user);
                if (!errors.IsNullOrEmpty())
                {
                    throw new RegisterUserException($"User isn't registered: {errors.First().Description}");
                }
                responce = await LoginByGoogle(user);
            }
            else
            {
                responce = await LoginByGoogle(searchedUser);
            }
            return responce;
        }

        private async Task<AuthResponceDto> LoginByGoogle(UserProfile user)
        {
            var token = await GenerateToken(user);
            return new AuthResponceDto()
            {
                UserEmail = user.Email,
                Token = token,
                RefreshToken = await CreateRefreshToken(user),
                Theme = _userThemeService.GetByUserId(user.Id).Result.Theme
            };
        }

        private async Task<List<IdentityError>> RegisterByGoogle(UserProfile user)
        {
            List<IdentityError> resultErrors = new List<IdentityError>();
            var result = await _userManager.CreateAsync(user, Guid.NewGuid().ToString() + "H");
            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!addToRoleResult.Succeeded)
                {
                    resultErrors.AddRange(addToRoleResult.Errors);
                }
                user.Theme = await _userThemeService.AddUserTheme(new AddUserThemeDto
                {
                    Theme = "dark",
                    UserId = user.Id
                });
            }
            else
            {
                resultErrors.AddRange(result.Errors);
            }
            return resultErrors;
        }

        private JwtSecurityToken ConvertOAuthTokenToJWT(string token)
        {
            var jwtSequrityTokenHandler = new JwtSecurityTokenHandler();
            if (!jwtSequrityTokenHandler.CanReadToken(token))
            {
                throw new InvalidTokenException("Token could not be read");
            }

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken();
            DateTime dateOfExpiring;
            try
            {
                var tokenContent = jwtSequrityTokenHandler.ReadJwtToken(token);
                dateOfExpiring = tokenContent.ValidTo;
                jwtSecurityToken = tokenContent;
            }
            catch (InvalidTokenException)
            {
                throw new InvalidTokenException("Invalid token!");
            }
            return jwtSecurityToken;
        }

        public async Task<string> GenerateResetPasswordToken(ForgotPasswordRequestDto requestDto)
        {
            var user = await _userManager.FindByEmailAsync(requestDto.UserEmail);
            string newResetPasswordToken;

            if (user is null)
            {
                throw new ObjectNotFoundException("User not found");
            }
            try
            {
                newResetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            }
            catch (InvalidTokenException)
            {
                throw new InvalidTokenException("Invalid reset password token!");
            }
            return newResetPasswordToken;
        }

        public async Task ResetPassword(string userEmail, string resetToken, ResetPasswordRequestDto requestDto)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (await _userManager.CheckPasswordAsync(user, requestDto.Password))
            {
                throw new ResetPasswordException("Old and new passwords are identical!");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetToken, requestDto.Password);
            if (!result.Succeeded)
            {
                throw new ResetPasswordException("Password cannot be reset");
            }
        }
    }
}
