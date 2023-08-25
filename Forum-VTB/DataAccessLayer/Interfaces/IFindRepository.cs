using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFindRepository : IRepository<Find>
    {
        Task<List<Find>> GetByUserId(string userId);

        Task Delete(string advertId);

        Task<Find> GetActiveById(string findId);

        Task<Find> GetById(string findId);

        Task<List<Find>> GetBySectionName(string sectionName);

        Task<List<Find>> GetBySubsectionName(string subsectionName, string sectionName);

        Task<List<Find>> SearchByKeyPhrase(string keyPhrase);
    }
}
