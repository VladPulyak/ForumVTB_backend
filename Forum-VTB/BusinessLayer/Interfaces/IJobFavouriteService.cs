using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Job;
using BusinessLayer.Dtos.JobFavourites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IJobFavouriteService
    {
        Task AddToJobFavourites(AddToJobFavouritesRequestDto requestDto);

        Task DeleteFromJobFavourites(DeleteFromJobFavouritesRequestDto requestDto);

        Task<List<JobResponceDto>> GetUserJobFavourites();
    }
}
