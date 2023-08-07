using BusinessLayer.Dtos.Advert;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAdvertService
    {
        Task<CreateAdvertResponceDto> CreateAdvert(CreateAdvertRequestDto requestDto);

        Task<UpdateAdvertResponceDto> UpdateAdvert(UpdateAdvertRequestDto requestDto);

        Task DeleteAdvert(DeleteAdvertRequestDto requestDto);

        Task<List<UserAdvertResponceDto>> GetUserAdverts();

        Task<GetAdvertCardResponceDto> GetAdvertCard(GetAdvertCardRequestDto requestDto);

        Task<List<UserAdvertResponceDto>> GetAllAdverts();

        Task<List<AdvertResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto);

        Task<List<AdvertResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto);

        Task<List<AdvertResponceDto>> GetFourNewestAdverts();
    }
}
