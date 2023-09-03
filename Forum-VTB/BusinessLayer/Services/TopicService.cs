using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Topic;
using BusinessLayer.Dtos.TopicFile;
using BusinessLayer.Dtos.TopicMessage;
using BusinessLayer.Interfaces;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly ITopicMessageService _topicMessageService;
        private readonly ITopicFileService _topicFileService;
        private readonly UserManager<UserProfile> _userManager;
        private readonly ISectionRepository _sectionRepository;
        private readonly ITopicFavouriteRepository _topicFavouriteRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public TopicService(ITopicRepository topicRepository, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IMapper mapper, ISubsectionRepository subsectionRepository, ITopicMessageService topicMessageService, ITopicFileService topicFileService, ISectionRepository sectionRepository, ITopicFavouriteRepository topicFavouriteRepository, IUserProfileRepository userProfileRepository)
        {
            _topicRepository = topicRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _subsectionRepository = subsectionRepository;
            _topicMessageService = topicMessageService;
            _topicFileService = topicFileService;
            _sectionRepository = sectionRepository;
            _topicFavouriteRepository = topicFavouriteRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<CreateTopicResponceDto> CreateTopic(CreateTopicRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var topic = _mapper.Map<Topic>(requestDto);
            var subsection = await _subsectionRepository.GetBySubsectionAndSectionNames(requestDto.SectionName, requestDto.SubsectionName);
            topic.Subsection = subsection;
            topic.UserId = user.Id;
            topic.MainPhoto = requestDto.MainPhoto;
            topic.DateOfCreation = DateTime.UtcNow;
            topic.Id = Guid.NewGuid().ToString();
            var addedTopic = await _topicRepository.Add(topic);
            await _topicRepository.Save();
            await _topicFileService.AddFiles(new AddTopicFilesRequestDto
            {
                TopicId = topic.Id,
                FileStrings = requestDto.FileStrings
            });

            return new CreateTopicResponceDto
            {
                TopicId = addedTopic.Id,
                Title = addedTopic.Title,
                Description = addedTopic.Description,
                Messages = new List<TopicMessage>(),
                DateOfCreation = topic.DateOfCreation,
                MainPhoto = addedTopic.MainPhoto,
                Files = await _topicFileService.GetTopicFiles(new GetTopicFileRequestDto
                {
                    TopicId = addedTopic.Id
                })
            };
        }

        public async Task<UpdateTopicResponceDto> UpdateTopic(UpdateTopicRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var topic = await _topicRepository.GetById(requestDto.TopicId);
            var subsection = await _subsectionRepository.GetBySubsectionAndSectionNames(requestDto.SectionName, requestDto.SubsectionName);
            topic.Title = requestDto.Title;
            topic.Description = requestDto.Description;
            topic.MainPhoto = requestDto.MainPhoto;
            topic.DateOfCreation = DateTime.UtcNow;
            topic.Subsection = subsection;
            topic.SubsectionId = subsection.Id;
            await _topicFileService.AddMissingFiles(new AddMissingTopicFilesRequestDto
            {
                Topic = topic,
                FileStrings = requestDto.FileStrings
            });
            var updatedTopic = _topicRepository.Update(topic);
            await _topicRepository.Save();
            var favourite = await _topicFavouriteRepository.GetByTopicAndUserIds(updatedTopic.Id, user.Id);
            bool isFavourite = favourite is not null ? true : false;
            return new UpdateTopicResponceDto
            {
                TopicId = updatedTopic.Id,
                Title = updatedTopic.Title,
                Description = updatedTopic.Description,
                DateOfCreation = updatedTopic.DateOfCreation,
                MainPhoto = updatedTopic.MainPhoto,
                Messages = updatedTopic.Messages is null ? new List<GetTopicMessageResponceDto>() :
                await _topicMessageService.GetTopicMessagesByTopicId(new GetTopicMessageRequestDto
                {
                    TopicId = updatedTopic.Id
                }),
                Files = await _topicFileService.GetTopicFiles(new GetTopicFileRequestDto
                {
                    TopicId = updatedTopic.Id
                }),
                IsFavourite = isFavourite
            };
        }

        public async Task<List<TopicResponceDto>> CheckFavourites(List<TopicResponceDto> responceDtos, string userId)
        {
            var favourites = await _topicFavouriteRepository.GetByUserId(userId);
            foreach (var responceDto in responceDtos)
            {
                if (favourites.SingleOrDefault(q => q.Id == responceDto.TopicId) is not null)
                {
                    responceDto.IsFavourite = true;
                }
            }

            return responceDtos;
        }

        public async Task DeleteTopic(DeleteTopicRequestDto requestDto)
        {
            await _topicRepository.Delete(requestDto.TopicId);
            await _topicRepository.Save();
        }

        public async Task<List<TopicResponceDto>> GetUserTopics()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userTopics = await _topicRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<TopicResponceDto>>(userTopics);
            return await CheckFavourites(responceDtos, user.Id);
        }

        public async Task<GetTopicCardResponceDto> GetTopicCard(GetTopicCardRequestDto requestDto)
        {
            bool isFavourite = false;
            var userTopic = await _topicRepository.GetById(requestDto.TopicId);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                var favourite = await _topicFavouriteRepository.GetByTopicAndUserIds(userTopic.Id, user.Id);
                isFavourite = favourite is not null ? true : false;
            }

            return new GetTopicCardResponceDto
            {
                TopicId = userTopic.Id,
                Title = userTopic.Title,
                Description = userTopic.Description,
                DateOfCreation = userTopic.DateOfCreation,
                Messages = userTopic.Messages is null ? new List<GetTopicMessageResponceDto>() :
                await _topicMessageService.GetTopicMessagesByTopicId(new GetTopicMessageRequestDto
                {
                    TopicId = userTopic.Id
                }),
                Files = await _topicFileService.GetTopicFiles(new GetTopicFileRequestDto
                {
                    TopicId = userTopic.Id
                }),
                NickName = userTopic.User.NickName,
                UserName = userTopic.User.UserName,
                UserPhoto = userTopic.User.Photo,
                SubsectionName = userTopic.Subsection.Name,
                IsFavourite = isFavourite,
                MainPhoto = userTopic.MainPhoto
            };
        }

        public async Task<List<UserTopicResponceDto>> GetAllTopics()
        {
            var topics = await _topicRepository.GetAll().ToListAsync();
            if (!topics.Any())
            {
                throw new ObjectNotFoundException("Topics not found");
            }

            var responceDtos = _mapper.Map<List<UserTopicResponceDto>>(topics);
            return responceDtos;
        }

        public async Task<List<TopicResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            var topics = await _topicRepository.GetBySectionName(requestDto.SectionName);
            var responceDtos = _mapper.Map<List<TopicResponceDto>>(topics.OrderByDescending(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<List<TopicResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            var topics = await _topicRepository.GetBySubsectionName(requestDto.SubsectionName, requestDto.SectionName);
            var responceDtos = _mapper.Map<List<TopicResponceDto>>(topics.OrderByDescending(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }
    }
}