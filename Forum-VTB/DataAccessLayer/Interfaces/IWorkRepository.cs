using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IWorkRepository : IRepository<Work>
    {
        Task<List<Work>> GetByUserId(string userId);

        Task Delete(string workId);

        Task<Work> GetById(string workId);

        Task<List<Work>> GetBySectionName(string sectionName);

        Task<List<Work>> GetBySubsectionName(string subsectionName);

        Task<List<Work>> SearchByKeyPhrase(string keyPhrase);

    }
}
