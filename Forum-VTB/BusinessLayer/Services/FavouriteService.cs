using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IAdvertFavouriteService _advertFavouriteService;
        private readonly IJobFavouriteService _jobFavouriteService;

        public FavouriteService(IAdvertFavouriteService advertFavouriteService, IJobFavouriteService jobFavouriteService)
        {
            _advertFavouriteService = advertFavouriteService;
            _jobFavouriteService = jobFavouriteService;
        }

        public async Task<GetUserFavouritesResponceDto> GetUserFavourites()
        {
            return new GetUserFavouritesResponceDto
            {
                Adverts = await _advertFavouriteService.GetUserAdvertFavourites(),
                Jobs = await _jobFavouriteService.GetUserJobFavourites()
            };
        }
    }
}
