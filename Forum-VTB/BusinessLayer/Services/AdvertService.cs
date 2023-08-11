using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFiles;
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
        private readonly IFavouriteRepository _favouriteRepository;

        public AdvertService(IAdvertRepository advertRepository, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IMapper mapper, ISubsectionRepository subsectionRepository, ICommentService commentService, IAdvertFileService fileService, IAdvertFileService advertFileService, ISectionRepository sectionRepository, IFavouriteRepository favouriteRepository)
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
                Files = await _advertFileService.GetAdvertFiles(new GetAdvertFileRequestDto
                {
                    AdvertId = addedAdvert.Id
                })
            };
        }

        public async Task<UpdateAdvertResponceDto> UpdateAdvert(UpdateAdvertRequestDto requestDto)
        {
            var advert = await _advertRepository.GetById(requestDto.AdvertId);
            advert.Title = requestDto.Title;
            advert.Description = requestDto.Description;
            advert.Price = requestDto.Price;
            advert.DateOfCreation = DateTime.UtcNow;
            await _advertFileService.AddMissingFiles(new AddMissingFilesRequestDto
            {
                Advert = advert,
                FileStrings = requestDto.FileStrings
            });
            var updatedAdvert = _advertRepository.Update(advert);
            await _advertRepository.Save();
            return new UpdateAdvertResponceDto
            {
                AdvertId = updatedAdvert.Id,
                Title = updatedAdvert.Title,
                Description = updatedAdvert.Description,
                Price = updatedAdvert.Price,
                DateOfCreation = updatedAdvert.DateOfCreation,
                Comments = updatedAdvert.Comments is null ? new List<GetCommentResponceDto>() :
                await _commentService.GetCommentsByAdvertId(new GetCommentsRequestDto
                {
                    AdvertId = updatedAdvert.Id
                }),
                Files = await _advertFileService.GetAdvertFiles(new GetAdvertFileRequestDto
                {
                    AdvertId = updatedAdvert.Id
                })
            };
        }

        public async Task<List<AdvertResponceDto>> GetFourNewestAdverts()
        {
            var adverts = await _advertRepository.GetAll().OrderByDescending(q => q.DateOfCreation).Take(4).ToListAsync();
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(adverts);
            return responceDtos;
        }

        public async Task DeleteAdvert(DeleteAdvertRequestDto requestDto)
        {
            await _advertRepository.Delete(requestDto.AdvertId);
            await _advertRepository.Save();
        }

        public async Task<List<UserAdvertResponceDto>> GetUserAdverts()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userAdverts = await _advertRepository.GetByUserId(user.Id);
            var userAdvertResponceDtos = _mapper.Map<List<UserAdvertResponceDto>>(userAdverts);
            return userAdvertResponceDtos;
        }

        public async Task<GetAdvertCardResponceDto> GetAdvertCard(GetAdvertCardRequestDto requestDto)
        {
            //var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            //var user = await _userManager.FindByEmailAsync(userEmail);
            var userAdvert = await _advertRepository.GetById(requestDto.AdvertId);
            //var favourite = await _favouriteRepository.GetByAdvertAndUserIds(userAdvert.Id, user.Id);

            return new GetAdvertCardResponceDto
            {
                AdvertId = userAdvert.Id,
                Title = userAdvert.Title,
                Description = userAdvert.Description,
                DateOfCreation = userAdvert.DateOfCreation,
                Price = userAdvert.Price,
                Comments = userAdvert.Comments is null ? new List<GetCommentResponceDto>() :
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
                SubsectionName = userAdvert.Subsection.Name
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
            return responceDtos;
        }

        public async Task<List<AdvertResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            var adverts = await _advertRepository.GetBySubsectionName(requestDto.SubsectionName);
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(adverts.OrderBy(q => q.DateOfCreation).ToList());
            return responceDtos;
        }
    }
}