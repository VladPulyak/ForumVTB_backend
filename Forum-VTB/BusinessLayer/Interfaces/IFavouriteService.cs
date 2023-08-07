using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Favourites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IFavouriteService
    {
        Task AddToFavourites(AddToFavouritesRequestDto requestDto);

        Task DeleteFromFavourites(DeleteFromFavouritesRequestDto requestDto);

        Task<List<AdvertResponceDto>> GetUserFavourites();
    }
}
