using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Dtos.Find;
using BusinessLayer.Dtos.FindFavourites;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.Services
{
    public class FindFavouriteService : IFindFavouriteService
    {
        private readonly IFindFavouriteRepository _findFavouriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IMapper _mapper;


        public FindFavouriteService(IFindFavouriteRepository findFavouriteRepository, IHttpContextAccessor httpContextAccessor, UserManager<UserProfile> userManager, IMapper mapper)
        {
            _findFavouriteRepository = findFavouriteRepository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddToFindFavourites(AddToFindFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var addedFavourite = await _findFavouriteRepository.Add(new FindFavourite
            {
                FindId = requestDto.FindId,
                UserId = user.Id,
                Id = Guid.NewGuid().ToString()
            });
            await _findFavouriteRepository.Save();
        }

        public async Task DeleteFromFindFavourites(DeleteFromFindFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _findFavouriteRepository.Delete(requestDto.FindId, user.Id);
            await _findFavouriteRepository.Save();
        }

        public async Task<List<FindResponceDto>> GetUserFindFavourites()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var finds = await _findFavouriteRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<FindResponceDto>>(finds);
            return responceDtos;
        }

    }
}
