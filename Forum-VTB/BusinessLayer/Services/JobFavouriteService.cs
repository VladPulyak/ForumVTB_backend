using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Job;
using BusinessLayer.Dtos.JobFavourites;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.Services
{
    public class JobFavouriteService : IJobFavouriteService
    {
        private readonly IJobFavouriteRepository _jobFavouriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IMapper _mapper;


        public JobFavouriteService(IJobFavouriteRepository jobFavouriteRepository, IHttpContextAccessor httpContextAccessor, UserManager<UserProfile> userManager, IMapper mapper)
        {
            _jobFavouriteRepository = jobFavouriteRepository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddToJobFavourites(AddToJobFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var addedFavourite = await _jobFavouriteRepository.Add(new JobFavourite
            {
                JobId = requestDto.JobId,
                UserId = user.Id,
                Id = Guid.NewGuid().ToString()
            });
            await _jobFavouriteRepository.Save();
        }

        public async Task DeleteFromJobFavourites(DeleteFromJobFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _jobFavouriteRepository.Delete(requestDto.JobId, user.Id);
            await _jobFavouriteRepository.Save();
        }

        public async Task<List<JobResponceDto>> GetUserJobFavourites()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var jobs = await _jobFavouriteRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<JobResponceDto>>(jobs);
            return responceDtos;
        }
    }
}
