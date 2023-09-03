using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Dtos.Topic;
using BusinessLayer.Dtos.TopicFavourite;
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
    public class TopicFavouriteService : ITopicFavouriteService
    {
        private readonly ITopicFavouriteRepository _topicFavouriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IMapper _mapper;


        public TopicFavouriteService(ITopicFavouriteRepository topicFavouriteRepository, IHttpContextAccessor httpContextAccessor, UserManager<UserProfile> userManager, IMapper mapper)
        {
            _topicFavouriteRepository = topicFavouriteRepository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddToTopicFavourites(AddToTopicFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var addedFavourite = await _topicFavouriteRepository.Add(new TopicFavourite
            {
                TopicId = requestDto.TopicId,
                UserId = user.Id,
                Id = Guid.NewGuid().ToString()
            });
            await _topicFavouriteRepository.Save();
        }

        public async Task DeleteFromTopicFavourites(DeleteFromTopicFavouritesRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _topicFavouriteRepository.Delete(requestDto.TopicId, user.Id);
            await _topicFavouriteRepository.Save();
        }

        public async Task<List<TopicResponceDto>> GetUserTopicFavourites()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var topics = await _topicFavouriteRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<TopicResponceDto>>(topics);
            return responceDtos;
        }

    }
}
