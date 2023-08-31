using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertFavourites;
using BusinessLayer.Dtos.Topic;
using BusinessLayer.Dtos.TopicFavourite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITopicFavouriteService
    {
        Task AddToTopicFavourites(AddToTopicFavouritesRequestDto requestDto);

        Task DeleteFromTopicFavourites(DeleteFromTopicFavouritesRequestDto requestDto);

        Task<List<TopicResponceDto>> GetUserTopicFavourites();
    }
}
