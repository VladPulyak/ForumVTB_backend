using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.Common;
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
using Telegram.Bot.Types;

namespace BusinessLayer.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IAdvertFileService _advertFileService;
        private readonly IAdvertFileService _fileService;
        private readonly UserManager<UserProfile> _userManager;
        private readonly ISectionRepository _sectionRepository;
        private readonly IAdvertFavouriteRepository _favouriteRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public AdvertService(IAdvertRepository advertRepository, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IMapper mapper, ISubsectionRepository subsectionRepository, ICommentService commentService, IAdvertFileService fileService, IAdvertFileService advertFileService, ISectionRepository sectionRepository, IAdvertFavouriteRepository favouriteRepository, IUserProfileRepository userProfileRepository)
        {
            _advertRepository = advertRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _subsectionRepository = subsectionRepository;
            _commentService = commentService;
            _fileService = fileService;
            _advertFileService = advertFileService;
            _sectionRepository = sectionRepository;
            _favouriteRepository = favouriteRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<CreateAdvertResponceDto> CreateAdvert(CreateAdvertRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var advert = _mapper.Map<Advert>(requestDto);
            var subsection = await _subsectionRepository.GetByName(requestDto.SubsectionName);
            advert.Subsection = subsection;
            advert.Price = requestDto.Price;
            advert.UserId = user.Id;
            advert.MainPhoto = requestDto.MainPhoto;
            advert.Status = Status.Active.ToString();
            advert.DateOfCreation = DateTime.UtcNow;
            advert.Id = Guid.NewGuid().ToString();
            advert.PhoneNumber = requestDto.PhoneNumber;
            var addedAdvert = await _advertRepository.Add(advert);
            await _advertRepository.Save();
            await _fileService.AddFiles(new AddAdvertFilesRequestDto
            {
                AdvertId = advert.Id,
                FileStrings = requestDto.FileStrings
            });

            return new CreateAdvertResponceDto
            {
                AdvertId = addedAdvert.Id,
                Title = addedAdvert.Title,
                Description = addedAdvert.Description,
                Price = addedAdvert.Price,
                Comments = new List<AdvertComment>(),
                DateOfCreation = advert.DateOfCreation,
                MainPhoto = addedAdvert.MainPhoto,
                Status = addedAdvert.Status,
                Files = await _advertFileService.GetAdvertFiles(new GetAdvertFileRequestDto
                {
                    AdvertId = addedAdvert.Id
                })
            };
        }

        public async Task<UpdateAdvertResponceDto> UpdateAdvert(UpdateAdvertRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var advert = await _advertRepository.GetActiveById(requestDto.AdvertId);
            advert.Title = requestDto.Title;
            advert.Description = requestDto.Description;
            advert.Price = requestDto.Price;
            advert.MainPhoto = requestDto.MainPhoto;
            advert.DateOfCreation = DateTime.UtcNow;
            advert.Status = Status.Active.ToString();
            await _advertFileService.AddMissingFiles(new AddMissingFilesRequestDto
            {
                Advert = advert,
                FileStrings = requestDto.FileStrings
            });
            var updatedAdvert = _advertRepository.Update(advert);
            await _advertRepository.Save();
            var favourite = await _favouriteRepository.GetByAdvertAndUserIds(updatedAdvert.Id, user.Id);
            bool isFavourite = favourite is not null ? true : false;
            return new UpdateAdvertResponceDto
            {
                AdvertId = updatedAdvert.Id,
                Title = updatedAdvert.Title,
                Description = updatedAdvert.Description,
                Price = updatedAdvert.Price,
                DateOfCreation = updatedAdvert.DateOfCreation,
                Status = updatedAdvert.Status,
                MainPhoto = updatedAdvert.MainPhoto,
                Comments = updatedAdvert.AdvertComments is null ? new List<GetCommentResponceDto>() :
                await _commentService.GetCommentsByAdvertId(new GetCommentsRequestDto
                {
                    AdvertId = updatedAdvert.Id
                }),
                Files = await _advertFileService.GetAdvertFiles(new GetAdvertFileRequestDto
                {
                    AdvertId = updatedAdvert.Id
                }),
                IsFavourite = isFavourite
            };
        }

        public async Task<List<AdvertResponceDto>> GetFourNewestAdverts()
        {
             var adverts = await _advertRepository.GetAll().OrderByDescending(q => q.DateOfCreation).Take(4).ToListAsync();
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(adverts);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<List<AdvertResponceDto>> CheckFavourites(List<AdvertResponceDto> responceDtos, string userId)
        {
            var favourites = await _favouriteRepository.GetByUserId(userId);
            foreach (var responceDto in responceDtos)
            {
                if (favourites.SingleOrDefault(q => q.Id == responceDto.AdvertId) is not null)
                {
                    responceDto.IsFavourite = true;
                }
            }

            return responceDtos;
        }

        public async Task DeleteAdvert(DeleteAdvertRequestDto requestDto)
        {
            await _advertRepository.Delete(requestDto.AdvertId);
            await _advertRepository.Save();
        }

        public async Task<List<AdvertResponceDto>> GetUserAdverts()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userAdverts = await _advertRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(userAdverts);
            return await CheckFavourites(responceDtos, user.Id);
        }

        public async Task<GetAdvertCardResponceDto> GetAdvertCard(GetAdvertCardRequestDto requestDto)
        {
            bool isFavourite = false;
            var userAdvert = await _advertRepository.GetActiveById(requestDto.AdvertId);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                var favourite = await _favouriteRepository.GetByAdvertAndUserIds(userAdvert.Id, user.Id);
                isFavourite = favourite is not null ? true : false;
            }

            return new GetAdvertCardResponceDto
            {
                AdvertId = userAdvert.Id,
                Title = userAdvert.Title,
                Description = userAdvert.Description,
                DateOfCreation = userAdvert.DateOfCreation,
                Price = userAdvert.Price,
                Comments = userAdvert.AdvertComments is null ? new List<GetCommentResponceDto>() :
                await _commentService.GetCommentsByAdvertId(new GetCommentsRequestDto
                {
                    AdvertId = userAdvert.Id
                }),
                Files = await _advertFileService.GetAdvertFiles(new GetAdvertFileRequestDto
                {
                    AdvertId = userAdvert.Id
                }),
                NickName = userAdvert.User.NickName,
                UserName = userAdvert.User.UserName,
                UserPhoto = userAdvert.User.Photo,
                SubsectionName = userAdvert.Subsection.Name,
                IsFavourite = isFavourite,
                PhoneNumber = userAdvert.PhoneNumber,
                MainPhoto = userAdvert.MainPhoto
            };
        }

        public async Task<List<UserAdvertResponceDto>> GetAllAdverts()
        {
            var adverts = await _advertRepository.GetAll().ToListAsync();
            if (!adverts.Any())
            {
                throw new ObjectNotFoundException("Adverts not found");
            }

            var userAdvertResponceDtos = _mapper.Map<List<UserAdvertResponceDto>>(adverts);
            return userAdvertResponceDtos;
        }

        public async Task<List<AdvertResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            var adverts = await _advertRepository.GetBySectionName(requestDto.SectionName);
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(adverts.OrderBy(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<List<AdvertResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            var adverts = await _advertRepository.GetBySubsectionName(requestDto.SubsectionName);
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(adverts.OrderBy(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task ChangeAdvertStatus(ChangeAdvertStatusRequestDto requestDto)
        {
            var userAdvert = await _advertRepository.GetById(requestDto.AdvertId);

            if (userAdvert.Status == "Active")
            {
                userAdvert.Status = Status.Disabled.ToString();
            }
            else
            {
                userAdvert.Status = Status.Active.ToString();
            }
            var updatedAdvert = _advertRepository.Update(userAdvert);
            await _advertRepository.Save();
        }
    }
}