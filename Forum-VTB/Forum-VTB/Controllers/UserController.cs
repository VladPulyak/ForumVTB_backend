using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Forum_VTB.MapProfiles;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Exceptions;
using BusinessLayer.Exceptions;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public UserController(UserManager<UserProfile> userManager, IMapper mapper, IUserService userService, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public ActionResult<UserRegisterDto> GetUsers()
        {
            var users = _userService.GetAll().ToList();
            return Ok(_mapper.Map<List<UserRegisterDto>>(users));
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPasswordByEmail(ResetPasswordRequestDto requestDto)
        {
            var provider = "Forum-VTB.LoginProvider";
            var resetPasswordToken = "Forum-VTB.ResetPasswordToken";

            var user = await _userManager.FindByEmailAsync(requestDto.Email);

            if (user is null)
            {
                return BadRequest("User not found");
            }

            try
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, provider, resetPasswordToken);
                var newResetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.SetAuthenticationTokenAsync(user, provider, resetPasswordToken, newResetPasswordToken);
                if (result.Succeeded)
                {
                    await _emailService.SendMessage(new EmailSenderDto
                    {
                        Subject = "Reset Password",
                        Body = $"https://f563-37-215-41-154.ngrok-free.app/User/ChangePassword?resetToken={newResetPasswordToken}",
                        ReceiverEmail = requestDto.Email
                    });
                    return Ok(newResetPasswordToken);
                }
                else
                {
                    throw new InvalidTokenException("Invalid reset token!");
                }
            }
            catch (InvalidTokenException)
            {
                throw;
            }
        }
    }
}
