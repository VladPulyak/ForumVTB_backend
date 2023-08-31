using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Find;
using BusinessLayer.Dtos.FindComments;
using BusinessLayer.Dtos.FindFIles;
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
    public class FindService : IFindService
    {
        private readonly IFindRepository _findRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly IFindCommentService _findCommentService;
        private readonly IFindFileService _findFileService;
        private readonly UserManager<UserProfile> _userManager;
        private readonly ISectionRepository _sectionRepository;
        private readonly IFindFavouriteRepository _findFavouriteRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public FindService(IFindRepository findRepository, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IMapper mapper, ISubsectionRepository subsectionRepository, IFindCommentService findCommentService, IFindFileService findFileService, ISectionRepository sectionRepository, IFindFavouriteRepository findFavouriteRepository, IUserProfileRepository userProfileRepository)
        {
            _findRepository = findRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _subsectionRepository = subsectionRepository;
            _findCommentService = findCommentService;
            _findFileService = findFileService;
            _sectionRepository = sectionRepository;
            _findFavouriteRepository = findFavouriteRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<CreateFindResponceDto> CreateFind(CreateFindRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var find = _mapper.Map<Find>(requestDto);
            var subsection = await _subsectionRepository.GetBySubsectionAndSectionNames(requestDto.SectionName, requestDto.SubsectionName);
            find.Subsection = subsection;
            find.UserId = user.Id;
            find.MainPhoto = requestDto.MainPhoto;
            find.Status = Status.Active.ToString();
            find.DateOfCreation = DateTime.UtcNow;
            find.Id = Guid.NewGuid().ToString();
            find.PhoneNumber = requestDto.PhoneNumber;
            var addedFind = await _findRepository.Add(find);
            await _findRepository.Save();
            await _findFileService.AddFiles(new AddFindFilesRequestDto
            {
                FindId = find.Id,
                FileStrings = requestDto.FileStrings
            });

            return new CreateFindResponceDto
            {
                FindId = addedFind.Id,
                Title = addedFind.Title,
                Description = addedFind.Description,
                Comments = new List<FindComment>(),
                DateOfCreation = find.DateOfCreation,
                MainPhoto = addedFind.MainPhoto,
                Status = addedFind.Status,
                Files = await _findFileService.GetFindFiles(new GetFindFileRequestDto
                {
                    FindId = addedFind.Id
                })
            };
        }

        public async Task<UpdateFindResponceDto> UpdateFind(UpdateFindRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var find = await _findRepository.GetById(requestDto.FindId);
            var subsection = await _subsectionRepository.GetBySubsectionAndSectionNames(requestDto.SectionName, requestDto.SubsectionName);
            find.Title = requestDto.Title;
            find.Description = requestDto.Description;
            find.MainPhoto = requestDto.MainPhoto;
            find.DateOfCreation = DateTime.UtcNow;
            find.Status = Status.Active.ToString();
            find.Subsection = subsection;
            find.SubsectionId = subsection.Id;
            await _findFileService.UpdateFindFiles(new UpdateFindFilesRequestDto
            {
                FindId = find.Id,
                FileStrings = requestDto.FileStrings
            });
            var updatedFind = _findRepository.Update(find);
            await _findRepository.Save();
            var favourite = await _findFavouriteRepository.GetByFindAndUserIds(updatedFind.Id, user.Id);
            bool isFavourite = favourite is not null ? true : false;
            return new UpdateFindResponceDto
            {
                FindId = updatedFind.Id,
                Title = updatedFind.Title,
                Description = updatedFind.Description,
                DateOfCreation = updatedFind.DateOfCreation,
                Status = updatedFind.Status,
                MainPhoto = updatedFind.MainPhoto,
                Comments = updatedFind.FindComments is null ? new List<GetFindCommentResponceDto>() :
                await _findCommentService.GetCommentsByFindId(new GetFindCommentRequestDto
                {
                    FindId = updatedFind.Id
                }),
                Files = await _findFileService.GetFindFiles(new GetFindFileRequestDto
                {
                    FindId = updatedFind.Id
                }),
                IsFavourite = isFavourite
            };
        }

        public async Task<List<FindResponceDto>> GetFourNewestFinds()
        {
            var finds = await _findRepository.GetAll().OrderByDescending(q => q.DateOfCreation).Take(4).ToListAsync();
            var responceDtos = _mapper.Map<List<FindResponceDto>>(finds);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<List<FindResponceDto>> CheckFavourites(List<FindResponceDto> responceDtos, string userId)
        {
            var favourites = await _findFavouriteRepository.GetByUserId(userId);
            foreach (var responceDto in responceDtos)
            {
                if (favourites.SingleOrDefault(q => q.Id == responceDto.FindId) is not null)
                {
                    responceDto.IsFavourite = true;
                }
            }

            return responceDtos;
        }

        public async Task DeleteFind(DeleteFindRequestDto requestDto)
        {
            await _findRepository.Delete(requestDto.FindId);
            await _findRepository.Save();
        }

        public async Task<List<FindResponceDto>> GetUserFinds()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userFinds = await _findRepository.GetByUserId(user.Id);
            var responceDtos = _mapper.Map<List<FindResponceDto>>(userFinds);
            return await CheckFavourites(responceDtos, user.Id);
        }

        public async Task<GetFindCardResponceDto> GetFindCard(GetFindCardRequestDto requestDto)
        {
            bool isFavourite = false;
            var userFind = await _findRepository.GetById(requestDto.FindId);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                var favourite = await _findFavouriteRepository.GetByFindAndUserIds(userFind.Id, user.Id);
                isFavourite = favourite is not null ? true : false;
            }

            return new GetFindCardResponceDto
            {
                FindId = userFind.Id,
                Title = userFind.Title,
                Description = userFind.Description,
                DateOfCreation = userFind.DateOfCreation,
                Comments = userFind.FindComments is null ? new List<GetFindCommentResponceDto>() :
                await _findCommentService.GetCommentsByFindId(new GetFindCommentRequestDto
                {
                    FindId = userFind.Id
                }),
                Files = await _findFileService.GetFindFiles(new GetFindFileRequestDto
                {
                    FindId = userFind.Id
                }),
                NickName = userFind.User.NickName,
                UserName = userFind.User.UserName,
                UserPhoto = userFind.User.Photo,
                SubsectionName = userFind.Subsection.Name,
                IsFavourite = isFavourite,
                PhoneNumber = userFind.PhoneNumber,
                MainPhoto = userFind.MainPhoto,
                Status = userFind.Status
            };
        }

        public async Task<List<UserFindResponceDto>> GetAllFinds()
        {
            var finds = await _findRepository.GetAll().ToListAsync();
            if (!finds.Any())
            {
                throw new ObjectNotFoundException("Finds not found");
            }

            var userAdvertResponceDtos = _mapper.Map<List<UserFindResponceDto>>(finds);
            return userAdvertResponceDtos;
        }

        public async Task<List<FindResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto)
        {
            var finds = await _findRepository.GetBySectionName(requestDto.SectionName);
            var responceDtos = _mapper.Map<List<FindResponceDto>>(finds.OrderByDescending(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<List<FindResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            var finds = await _findRepository.GetBySubsectionName(requestDto.SubsectionName, requestDto.SectionName);
            var responceDtos = _mapper.Map<List<FindResponceDto>>(finds.OrderByDescending(q => q.DateOfCreation).ToList());
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task ChangeFindStatus(ChangeFindStatusRequestDto requestDto)
        {
            var userFind = await _findRepository.GetById(requestDto.FindId);

            if (userFind.Status == "Active")
            {
                userFind.Status = Status.Disabled.ToString();
            }
            else
            {
                userFind.Status = Status.Active.ToString();
            }
            _findRepository.Update(userFind);
            await _findRepository.Save();
        }

    }
}
