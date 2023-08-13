using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Common;
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
    public class CommonService : ICommonService
    {
        private readonly IMapper _mapper;
        private readonly IAdvertRepository _advertRepository;
        private readonly IAdvertService _advertService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;

        public CommonService(IAdvertRepository advertRepository, IMapper mapper, IAdvertService advertService, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager)
        {
            _advertRepository = advertRepository;
            _mapper = mapper;
            _advertService = advertService;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        private async Task<List<AdvertResponceDto>> GetAdvertByKeyPhrase(string keyPhrase)
        {
            var adverts = await _advertRepository.SearchByKeyPhrase(keyPhrase);
            var responceDtos = _mapper.Map<List<AdvertResponceDto>>(adverts);
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return await _advertService.CheckFavourites(responceDtos, user.Id);
            }
            return responceDtos;
        }

        public async Task<SearchByKeyPhraseResponceDto> GetByKeyPhrase(string keyPhrase)
        {
            return new SearchByKeyPhraseResponceDto
            {
                Adverts = await GetAdvertByKeyPhrase(keyPhrase)
            };
        }
    }
}
