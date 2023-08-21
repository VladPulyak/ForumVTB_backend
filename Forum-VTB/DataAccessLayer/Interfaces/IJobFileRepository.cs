using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IJobFileRepository : IRepository<JobFile>
    {
        Task<JobFile> GetById(string id);

        Task<List<JobFile>> GetByJobId(string jobId);

        Task Delete(string fileId);
    }
}
