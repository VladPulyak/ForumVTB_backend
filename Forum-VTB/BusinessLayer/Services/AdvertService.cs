using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
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
        private readonly UserManager<UserProfile> _userManager;

        public AdvertService(IAdvertRepository advertRepository, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IMapper mapper, ISubsectionRepository subsectionRepository, ICommentService commentService)
        {
            _advertRepository = advertRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _subsectionRepository = subsectionRepository;
            _commentService = commentService;
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
            advert.DateOfCreation = DateTime.Now;
            advert.Id = Guid.NewGuid().ToString();
            var addedAdvert = await _advertRepository.Add(advert);
            await _advertRepository.Save();
            return new CreateAdvertResponceDto
            {
                AdvertId = addedAdvert.Id,
                Title = addedAdvert.Title,
                Description = addedAdvert.Description,
                Price = addedAdvert.Price,
                Comments = addedAdvert.Comments is null ? new List<AdvertComment>() : addedAdvert.Comments.ToList(),
                DateOfCreation = advert.DateOfCreation
            };
        }

        public async Task<UpdateAdvertResponceDto> UpdateAdvert(UpdateAdvertRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var advert = await _advertRepository.GetById(requestDto.AdvertId);
            advert.Title = requestDto.Title;
            advert.Description = requestDto.Description;
            advert.Price = requestDto.Price;
            advert.DateOfCreation = DateTime.Now;
            var updatedAdvert = _advertRepository.Update(advert);
            await _advertRepository.Save();
            return new UpdateAdvertResponceDto
            {
                AdvertId = updatedAdvert.Id,
                Title = updatedAdvert.Title,
                Description = updatedAdvert.Description,
                Price = updatedAdvert.Price,
                Comments = updatedAdvert.Comments is null ? new List<AdvertComment>() : updatedAdvert.Comments.ToList(),
                DateOfCreation = updatedAdvert.DateOfCreation
            };
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
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userAdvert = await _advertRepository.GetById(requestDto.AdvertId);

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
                Files = userAdvert.Files is null ? new List<AdvertFile>() : userAdvert.Files.ToList(),
                NickName = user.NickName,
                UserName = user.UserName,
                UserPhoto = user.Photo,
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
    }
}
