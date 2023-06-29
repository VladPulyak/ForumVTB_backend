using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Exceptions;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;
        public UserController(IMapper mapper, IUserService userService, IEmailService emailService, IAuthService authService)
        {
            _mapper = mapper;
            _userService = userService;
            _emailService = emailService;
            _authService = authService;
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public ActionResult<UserRegisterDto> GetUsers()
        {
            var users = _userService.GetAll().ToList();
            return Ok(_mapper.Map<List<UserRegisterDto>>(users));
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequestDto requestDto)
        {
            string newResetPasswordToken;
            try
            {
                newResetPasswordToken = await _authService.GenerateResetPasswordToken(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await _emailService.SendMessage(new EmailSenderDto
            {
                Subject = "Reset Password",
                Body = "https://localhost:7086" + Url.Action("ResetPassword", new { userEmail = requestDto.UserEmail, resetToken = newResetPasswordToken }),
                ReceiverEmail = requestDto.UserEmail
            });

            return Ok("To reset your password, follow the link sent to your email");
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromQuery] string userEmail, [FromQuery] string resetToken, [FromForm] ResetPasswordRequestDto requestDto)
        {
            try
            {
                await _authService.ResetPassword(userEmail, resetToken, requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Password reset successfully!");
        }
    }
}
