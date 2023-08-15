using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<List<Advert>> GetByUserId(string userId);

        Task Delete(string advertId);

        Task<Advert> GetById(string advertId);

        Task<List<Advert>> GetBySectionName(string sectionName);

        Task<List<Advert>> GetBySubsectionName(string subsectionName);

        Task<List<Advert>> SearchByKeyPhrase(string keyPhrase);
    }
}
