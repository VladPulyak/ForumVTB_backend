using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IJobFavouriteRepository : IRepository<JobFavourite>
    {
        Task<JobFavourite> GetByJobAndUserIds(string jobId, string userId);

        Task Delete(string jobId, string userId);

        Task<List<Job>> GetByUserId(string userId);
    }
}
