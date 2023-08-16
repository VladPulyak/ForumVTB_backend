using BusinessLayer.Dtos.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IWorkService
    {
        Task<CreateAdvertResponceDto> CreateAdvert(CreateAdvertRequestDto requestDto);

        Task<UpdateAdvertResponceDto> UpdateAdvert(UpdateAdvertRequestDto requestDto);

        Task DeleteAdvert(DeleteAdvertRequestDto requestDto);

        Task<List<AdvertResponceDto>> GetUserAdverts();

        Task<GetAdvertCardResponceDto> GetAdvertCard(GetAdvertCardRequestDto requestDto);

        Task<List<UserAdvertResponceDto>> GetAllAdverts();

        Task<List<AdvertResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto);

        Task<List<AdvertResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto);

        Task<List<AdvertResponceDto>> GetFourNewestAdverts();

        Task<List<AdvertResponceDto>> CheckFavourites(List<AdvertResponceDto> responceDtos, string userId);

        Task ChangeAdvertStatus(ChangeAdvertStatusRequestDto requestDto);
    }
}
