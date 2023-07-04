using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using System.Text.Json;
using BusinessLayer.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DataAccessLayer.Models;
using BusinessLayer.Services;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegistration)
        {
            var userRegisterResponceDto = await _authService.Register(userRegistration);

            if (userRegisterResponceDto.Errors.Any())
            {
                foreach (var error in userRegisterResponceDto.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(userRegisterResponceDto);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponceDto>> Login([FromBody] UserLoginDto userLogin)
        {
            AuthResponceDto result;
            try
            {
                result = await _authService.Login(userLogin);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            if (result is null)
            {
                return Unauthorized(result.UserEmail);
            }
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthResponceDto>> RefreshToken([FromBody] AuthResponceDto request)
        {
            var result = await _authService.RefreshToken(request);
            if (result is null)
            {
                return Unauthorized(result.UserEmail);
            }

            return Ok(result);
        }

        [HttpPost("GoogleAuthentication")]
        public async Task<ActionResult<AuthResponceDto>> GoogleAuthentication([FromBody] object request)
        {
            var requestDto = JsonSerializer.Deserialize<GoogleAuthRequestDto>(request.ToString(), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            AuthResponceDto responce;

            try
            {
                responce = await _authService.GoogleAuthentication(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(responce);
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