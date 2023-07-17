using BusinessLayer.Dtos.Account;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<UserProfileInfoResponceDto> FillingAccountInfo(FillingAccountDataRequestDto requestDto);

        Task<string> ChangePassword(ChangePasswordRequestDto requestDto);

        Task<string> CheckPassword(CheckPasswordRequestDto requestDto);

        Task ChangePhoneNumber(ChangePhoneNumberRequestDto requestDto);

        Task<UserProfileInfoResponceDto> GetUserProfile();

        Task Support(SupportMessageRequestDto requestDto);

    }
}
