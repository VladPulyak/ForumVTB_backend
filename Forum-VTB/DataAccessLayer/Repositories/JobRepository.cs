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
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public override IQueryable<Job> GetAll()
        {
            return _set.Where(q => q.Status == "Active")
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Include(q => q.Favourites)
                .AsNoTracking();
        }

        public async Task Delete(string jobId)
        {
            var entity = await GetById(jobId);
            _set.Remove(entity);
        }

        public async Task<List<Job>> GetByUserId(string userId)
        {
            var jobs = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .OrderBy(q => q.DateOfCreation)
                .ToListAsync();
            return jobs;
        }

        public async Task<Job> GetActiveById(string jobId)
        {
            var job = await _set.Where(q => q.Id == jobId)
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Files)
                .SingleAsync();
            if (job is null)
            {
                throw new ObjectNotFoundException("Job with this id is not found");
            }

            return job;
        }

        public async Task<Job> GetById(string jobId)
        {
            var job = await _set.Where(q => q.Id == jobId)
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Files)
                .SingleAsync();
            if (job is null)
            {
                throw new ObjectNotFoundException("Job with this id is not found");
            }

            return job;

        }

        public async Task<List<Job>> GetBySectionName(string sectionName)
        {
            var jobs = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Section.Name == sectionName && q.Status == "Active")
                .ToListAsync();

            return jobs;
        }

        public async Task<List<Job>> GetBySubsectionName(string subsectionName)
        {
            var jobs = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Name == subsectionName && q.Status == "Active")
                .ToListAsync();

            return jobs;
        }

        public async Task<List<Job>> SearchByKeyPhrase(string keyPhrase)
        {
            keyPhrase = keyPhrase.Trim();
            return await _set.Where(a => (a.Title.ToUpper().Contains(keyPhrase.ToUpper()) || a.Description.ToUpper().Contains(keyPhrase.ToUpper())) && a.Status == "Active")
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .ToListAsync();
        }
    }
}
