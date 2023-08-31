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
    public class JobFileRepository : Repository<JobFile>, IJobFileRepository
    {
        public JobFileRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<JobFile> GetById(string id)
        {
            var file = await _set.Where(q => q.Id == id).Include(q => q.Job).SingleAsync();

            if (file is null)
            {
                throw new ObjectNotFoundException("File not found");
            }

            return file;

        }

        public async Task<List<JobFile>> GetByJobId(string jobId)
        {
            var files = await _set.Where(q => q.JobId == jobId).Include(q => q.Job).ToListAsync();
            return files;
        }

        public async Task Delete(string fileId)
        {
            var entity = await GetById(fileId);
            _set.Remove(entity);
        }

        public async Task DeleteRange(string jobId)
        {
            var files = await _set.Where(q => q.JobId == jobId).ToArrayAsync();
            _set.RemoveRange(files);
        }
    }
}
