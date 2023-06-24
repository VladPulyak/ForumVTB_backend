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

            var result = await _authService.Login(userLogin);
            if (result is null)
            {
                return Unauthorized(result.UserEmail);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/auth/userinfo.profile")]
        public ActionResult GetProfile(string userProfile)
        {
            return Ok(userProfile);
        }

        [HttpGet]
        [Route("/auth/userinfo.email")]
        public ActionResult GetEmail(string email)
        {
            return Ok(email);
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


        [HttpPost("Google")]
        [Authorize]
        public ActionResult GetUsers()
        {
            return Ok(_userService.GetAll());
        }
    }
}