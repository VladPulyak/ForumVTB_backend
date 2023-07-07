using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AutoMapper;

namespace BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        public AccountService(IHttpContextAccessor contextAccessor, IUserService userService, UserManager<UserProfile> userManager, IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserProfileInfoResponceDto> FillingAccountInfo(FillingAccountDataRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var searchedUserByUserName = await _userManager.FindByNameAsync(requestDto.UserName);
            if (searchedUserByUserName is not null && searchedUserByUserName.Id != user.Id)
            {
                throw new DuplicateUserException("User with this username already exists");
            }
            user.UserName = requestDto.UserName;
            user.NickName = requestDto.NickName;
            user.BirthDate = new DateTime(requestDto.YearOfBirth, requestDto.MonthOfBirth, requestDto.DayOfBirth);
            user.Photo = requestDto.Photo;
            var updatedUser = _userService.Update(user);
            await _userService.Save();
            return new UserProfileInfoResponceDto
            {
                Photo = updatedUser.Photo,
                BirthDate = updatedUser.BirthDate,
                NickName = updatedUser.NickName,
                UserName = updatedUser.UserName
            };
        }

        public async Task<UserProfileInfoResponceDto> GetUserProfile()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var responceDto = _mapper.Map<UserProfileInfoResponceDto>(user);
            return responceDto;
        }

        public async Task ChangePassword(ChangePasswordRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.ChangePasswordAsync(user, requestDto.OldPassword, requestDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new ChangePasswordException("Password cannot be changed");
            }
        }

        public async Task ChangePhoneNumber(ChangePhoneNumberRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.ChangePhoneNumberAsync(user, requestDto.NewPhoneNumber, await _userManager.GenerateChangePhoneNumberTokenAsync(user, requestDto.NewPhoneNumber));
            if (!result.Succeeded)
            {
                throw new ChangePhoneNumberException("Phone number cannot be changed");
            }
        }
    }
}
