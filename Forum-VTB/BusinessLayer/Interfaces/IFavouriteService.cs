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
        Task AddToAdvertFavourites(AddToFavouritesRequestDto requestDto);

        Task DeleteFromAdvertFavourites(DeleteFromFavouritesRequestDto requestDto);

        Task<GetUserFavouritesResponceDto> GetUserFavourites();
    }
}
