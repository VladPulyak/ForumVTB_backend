using BusinessLayer.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICommonService
    {
        Task<SearchByKeyPhraseResponceDto> GetByKeyPhrase(string keyPhrase);

        Task<GetAllUserAdvertsResponceDto> GetUserAdverts();

        Task<GetUserFavouritesResponceDto> GetUserFavourites();
    }
}
