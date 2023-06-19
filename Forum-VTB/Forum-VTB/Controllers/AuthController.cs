using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public Task<ActionResult<UserLoginDto>> Register(UserRegisterDto userRequestDto)
        {

        }
    }
}
