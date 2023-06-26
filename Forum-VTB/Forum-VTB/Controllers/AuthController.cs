using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IUserService userService, IConfiguration configuration)
        {
            _authService = authService;
            _userService = userService;
            _configuration = configuration;
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
            var result = await _authService.VerifyRefreshToken(request);
            if (result is null)
            {
                return Unauthorized(result.UserEmail);
            }

            return Ok(result);
        }


        //[HttpPost("Google")]
        //public ActionResult Google(object obj)
        //{
        //    Console.WriteLine(obj);
        //    return Ok();
        //}
    }
}