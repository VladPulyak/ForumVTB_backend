using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using System.Text.Json;
using BusinessLayer.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DataAccessLayer.Models;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
    }
}