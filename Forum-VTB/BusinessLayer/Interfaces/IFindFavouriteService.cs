using BusinessLayer.Dtos.Find;
using BusinessLayer.Dtos.FindFavourites;

namespace BusinessLayer.Interfaces
{
    public interface IFindFavouriteService
    {
        Task AddToFindFavourites(AddToFindFavouritesRequestDto requestDto);

        Task DeleteFromFindFavourites(DeleteFromFindFavouritesRequestDto requestDto);

        Task<List<FindResponceDto>> GetUserFindFavourites();
    }
}
