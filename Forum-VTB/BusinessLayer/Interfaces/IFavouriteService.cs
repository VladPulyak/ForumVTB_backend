using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IFavouriteService
    {
        Task AddToAdvertFavourites(AddToFAdvertFavouritesRequestDto requestDto);

        Task DeleteFromAdvertFavourites(DeleteFromAdvertFavouritesRequestDto requestDto);

        Task<GetUserFavouritesResponceDto> GetUserFavourites();
    }
}
