using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Favourites;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IMapper _mapper;

        public FavouriteService(IFavouriteRepository favouriteRepository, IHttpContextAccessor httpContextAccessor, UserManager<UserProfile> userManager, IMapper mapper)
        {
            _favouriteRepository = favouriteRepository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddToFavourites(AddToFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var addedFavourite = await _favouriteRepository.Add(new Favourite
            {
                AdvertId = requestDto.AdvertId,
                UserId = user.Id,
                Id = Guid.NewGuid().ToString()
            });
            await _favouriteRepository.Save();
        }

        public async Task DeleteFromFavourites(DeleteFromFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _favouriteRepository.Delete(requestDto.AdvertId, user.Id);
            await _favouriteRepository.Save();
        }

        public async Task<List<AdvertResponceDto>> GetUserFavourites()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var adverts = await _favouriteRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(adverts);
            return responceDtos;
        }
    }
}
