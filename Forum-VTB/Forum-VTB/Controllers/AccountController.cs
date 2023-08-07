using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.UserThemes;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum_VTB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserThemeService _userThemeService;

        public AccountController(IAccountService accountService, IUserThemeService userThemeService)
        {
            _accountService = accountService;
            _userThemeService = userThemeService;
        }

        [HttpGet("GetUserProfile")]
        public async Task<ActionResult> GetUserProfile()
        {
            var responceDto = await _accountService.GetUserProfile();
            return Ok(responceDto);
        }

        [HttpPost("FillingAccountInfo")]
        public async Task<ActionResult> FillingAccountInfo(FillingAccountDataRequestDto requestDto)
        {
            UserProfileInfoResponceDto responceDto;
            try
            {
                responceDto = await _accountService.FillingAccountInfo(requestDto);

            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(responceDto);
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequestDto requestDto)
        {
            try
            {
                await _accountService.ChangePassword(requestDto);
            }
            catch (ChangePasswordException ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok("Password changed successfuly!");
        }

        [HttpPost("CheckPassword")]
        public async Task<ActionResult> CheckPassword(CheckPasswordRequestDto requestDto)
        {
            string resultString;
            try
            {
                resultString = await _accountService.CheckPassword(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(resultString);
        }

        [HttpPost("ChangePhoneNumber")]
        public async Task<ActionResult> ChangePhoneNumber(ChangePhoneNumberRequestDto requestDto)
        {
            try
            {
                await _accountService.ChangePhoneNumber(requestDto);
            }
            catch (ChangePhoneNumberException ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok("Phone number changed successfully!");

        }

        [HttpPost("Support")]
        public async Task<ActionResult> Support(SupportMessageRequestDto requestDto)
        {
            try
            {
                await _accountService.Support(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok("Message sent successfully!");
        }

        [HttpPatch("/Theme/ChangeTheme")]
        public async Task<ActionResult> ChangeTheme(UserThemeDto requestDto)
        {
            try
            {
                await _userThemeService.ChangeTheme(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Theme changed successfully!");
        }
    }
}
