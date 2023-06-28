using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Forum_VTB.MapProfiles;
using Microsoft.AspNetCore.Authorization;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(UserManager<UserProfile> userManager, IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public ActionResult<UserRegisterDto> GetUsers()
        {
            var users = _userService.GetAll().ToList();
            return Ok(_mapper.Map<List<UserRegisterDto>>(users));
        }
    }
}
