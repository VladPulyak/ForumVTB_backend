using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using System.Text.Json;
using BusinessLayer.Dtos.Authentication;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Email;

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
        public async Task<ActionResult> Register(UserRegisterDto userRegistration)
        {
            UserRegisterResponceDto responceDto;
            try
            {
                responceDto = await _authService.Register(userRegistration);

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

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponceDto>> Login(UserLoginDto userLogin)
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

            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthResponceDto>> RefreshToken([FromBody] AuthResponceDto request)
        {
            AuthResponceDto responceDto;
            try
            {
                responceDto = await _authService.RefreshToken(request);
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
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
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
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            //var domain = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;
            var domain = "http://10.55.1.8:90";
            //var domain = "https://localhost:3000";

            await _emailService.SendMessage(new EmailSenderDto
            {
                Subject = "Reset Password",
                Body = domain + Url.Action("ResetPassword", new { userEmail = requestDto.UserEmail, resetToken = newResetPasswordToken }),
                ReceiverEmail = requestDto.UserEmail
            });

            return Ok("To reset your password, follow the link sent to your email");
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(string userEmail, string resetToken, ResetPasswordRequestDto requestDto)
        {
            try
            {
                await _authService.ResetPassword(userEmail, resetToken, requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Password reset successfully!");
        }
    }
}