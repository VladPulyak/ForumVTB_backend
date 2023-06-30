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
        public UserController(IMapper mapper, IUserService userService)
        {
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
