using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AutoMapper;
using System.Net.Mail;
using System.Net;
using System.Text;

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

        public async Task<string> ChangePassword(ChangePasswordRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (await _userManager.CheckPasswordAsync(user, requestDto.NewPassword))
            {
                throw new ChangePasswordException("Passwords are identical!");
            }

            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (removePasswordResult.Succeeded)
            {
                var additionPasswordResult = await _userManager.AddPasswordAsync(user, requestDto.NewPassword);
                if (additionPasswordResult.Succeeded)
                {
                    return "Password changed successfully!";
                }
                else
                {
                    throw new ChangePasswordException("Password cannot be changed");
                }
            }
            else
            {
                throw new ChangePasswordException("Password cannot be changed");
            }
        }

        public async Task<string> CheckPassword(CheckPasswordRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            bool isVerified = await _userManager.CheckPasswordAsync(user, requestDto.OldPassword);
            if (!isVerified)
            {
                throw new ChangePasswordException("Password is incorrect");
            }
            return "Password is valid";
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

        public async Task Support(SupportMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            string senderEmail = "forumvtbds@gmail.com";
            string senderPassword = "mzioajhajzrrhikz";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            mailMessage.To.Add(new MailAddress("forumvtbds@gmail.com"));
            mailMessage.Subject = "Support";
            mailMessage.Body = requestDto.Text + $"\n Message from {userEmail}";
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;
            foreach (var fileString in requestDto.Files)
            {
                byte[] bytes = Convert.FromBase64String(fileString);
                mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), "hello.png"));
            }
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            await smtpClient.SendMailAsync(mailMessage);

        }
    }
}
