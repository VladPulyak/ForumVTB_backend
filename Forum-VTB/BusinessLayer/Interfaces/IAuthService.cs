using BusinessLayer.Dtos;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
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
        //string CreateAccessToken(UserProfile userProfile);

        //UserProfile MapUserProfile(UserRegisterDto userRequestDto);

        //void SetRefreshToken(RefreshToken refreshToken, UserProfile userProfile);

        //RefreshToken GenerateRefreshToken();

        Task<UserRegisterResponceDto> Register(UserRegisterDto registerUserDto);

        Task<AuthResponceDto> Login(UserLoginDto loginUserDto);

        Task<string> CreateRefreshToken(UserProfile user);

        Task<AuthResponceDto> VerifyRefreshToken(AuthResponceDto request);
    }
}
