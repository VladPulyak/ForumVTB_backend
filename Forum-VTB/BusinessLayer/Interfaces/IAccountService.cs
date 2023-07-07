using BusinessLayer.Dtos;
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
        Task ChangePassword(ChangePasswordRequestDto requestDto);

        Task ChangePhoneNumber(ChangePhoneNumberRequestDto requestDto);

        Task<UserProfileInfoResponceDto> GetUserProfile();
    }
}
