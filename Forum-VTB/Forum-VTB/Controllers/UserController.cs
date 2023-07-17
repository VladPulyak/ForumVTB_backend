using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Exceptions;
using DataAccessLayer.InfoModels;
using DataAccessLayer.Interfaces;
using BusinessLayer.Dtos.Authentication;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IReadOnlyRepository<CarsInfo> _carsInfoRepository;
        public UserController(IMapper mapper, IUserService userService, IReadOnlyRepository<CarsInfo> carsInfoRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _carsInfoRepository = carsInfoRepository;
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
