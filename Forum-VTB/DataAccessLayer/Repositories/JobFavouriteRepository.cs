using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class JobFavouriteRepository : Repository<JobFavourite>, IJobFavouriteRepository
    {
        public JobFavouriteRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<JobFavourite> GetByJobAndUserIds(string jobId, string userId)
        {
            var favourite = await _set.Where(q => q.JobId == jobId && q.UserId == userId)
                .SingleOrDefaultAsync();
            return favourite;
        }

        public async Task Delete(string jobId, string userId)
        {
            var favourite = await GetByJobAndUserIds(jobId, userId);
            if (favourite is null)
            {
                throw new ObjectNotFoundException("Object with this jobId or userId is not found");
            }
            _set.Remove(favourite);
        }

        public async Task<List<Job>> GetByUserId(string userId)
        {
            var jobs = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Job.Files)
                .Include(q => q.Job.Subsection)
                .Include(q => q.Job.Subsection.Section)
                .Select(q => q.Job)
                .ToListAsync();
            return jobs;
        }
    }
}
