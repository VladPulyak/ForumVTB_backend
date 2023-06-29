using BusinessLayer.Dtos;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        Task<UserRegisterResponceDto> Register(UserRegisterDto registerUserDto);

        Task<AuthResponceDto> Login(UserLoginDto loginUserDto);

        Task<string> CreateRefreshToken(UserProfile user);

        Task<AuthResponceDto> RefreshToken(AuthResponceDto request);

        Task<AuthResponceDto> GoogleAuthentication(GoogleAuthRequestDto requestDto);

        Task<string> GenerateResetPasswordToken(ForgotPasswordRequestDto requestDto);

        Task ResetPassword(string userEmail, string resetToken, ResetPasswordRequestDto requestDto);
    }
}
