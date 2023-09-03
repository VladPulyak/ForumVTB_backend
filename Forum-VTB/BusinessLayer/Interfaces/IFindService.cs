using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Find;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IFindService
    {
        Task<CreateFindResponceDto> CreateFind(CreateFindRequestDto requestDto);

        Task<UpdateFindResponceDto> UpdateFind(UpdateFindRequestDto requestDto);

        Task DeleteFind(DeleteFindRequestDto requestDto);

        Task<List<FindResponceDto>> GetUserFinds();

        Task<GetFindCardResponceDto> GetFindCard(GetFindCardRequestDto requestDto);

        Task<List<UserFindResponceDto>> GetAllFinds();

        Task<List<FindResponceDto>> FindBySectionName(FindBySectionNameRequestDto requestDto);

        Task<List<FindResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto);

        Task<List<FindResponceDto>> GetFourNewestFinds();

        Task<List<FindResponceDto>> CheckFavourites(List<FindResponceDto> responceDtos, string userId);

        Task ChangeFindStatus(ChangeFindStatusRequestDto requestDto);
    }
}
