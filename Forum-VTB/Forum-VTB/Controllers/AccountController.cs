using BusinessLayer.Dtos;
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
        private readonly UserManager<UserProfile> _userManager;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        public AccountController(UserManager<UserProfile> userManager, IUserService userService, IAccountService accountService)
        {
            _userManager = userManager;
            _userService = userService;
            _accountService = accountService;
        }
        [HttpGet("GetProfile")]
        public async Task<ActionResult<UserProfile>> GetUserProfile()
        {
            var userEmail = User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user);
        }

        [HttpPost("FillingAccountInfo")]
        public async Task<ActionResult> FillingAccountInfo([FromForm] FillingAccountDataRequestDto requestDto)
        {
            var userEmail = User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            user.UserName = requestDto.UserName;
            user.NickName = requestDto.NickName;
            user.BirthDate = new DateTime(requestDto.YearOfBirth, requestDto.MonthOfBirth, requestDto.DayOfBirth);
            user.Photo = "photo";
            //Photo
            var updatedUser = _userService.Update(user);
            await _userService.Save();
            return Ok(updatedUser);
        }

        [HttpPost("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailRequestDto requestDto)
        {
            var userEmail = User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userWithNewEmail = await _userManager.FindByEmailAsync(requestDto.NewEmail);
            if (userWithNewEmail is not null)
            {
                return BadRequest("This email is in user by another user");
            }

            var result = await _userManager.ChangeEmailAsync(user, requestDto.NewEmail, await _userManager.GenerateChangeEmailTokenAsync(user, requestDto.NewEmail));

            if (result.Succeeded)
            {
                return Ok("Email changed successfully");
            }
            else
            {
                throw new EmailChangingException("Email cannot be changed");
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequestDto requestDto)
        {
            var userEmail = User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.ChangePasswordAsync(user, requestDto.OldPassword, requestDto.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password changed successfully!");
            }
            else
            {
                return BadRequest("Password cannot be changed!");
            }
        }

        [HttpPost("ChangePhoneNumber")]
        public async Task<ActionResult> ChangePhoneNumber(ChangePhoneNumberRequestDto requestDto)
        {
            var userEmail = User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.ChangePhoneNumberAsync(user, requestDto.NewPhoneNumber, await _userManager.GenerateChangePhoneNumberTokenAsync(user, requestDto.NewPhoneNumber));
            if (result.Succeeded)
            {
                return Ok("Phone number changed successfully!");
            }
            else
            {
                return BadRequest("Phone number cannot be changed");
            }
        }
    }
}
