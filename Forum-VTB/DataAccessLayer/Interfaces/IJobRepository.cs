using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<List<Job>> GetByUserId(string userId);

        Task Delete(string jobId);

        Task<Job> GetById(string jobId);

        Task<Job> GetActiveById(string jobId);

        Task<List<Job>> GetBySectionName(string sectionName);

        Task<List<Job>> GetBySubsectionName(string subsectionName);

        Task<List<Job>> SearchByKeyPhrase(string keyPhrase);

    }
}
