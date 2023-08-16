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
    public class WorkFileRepository : Repository<WorkFile>, IWorkFileRepository
    {
        public WorkFileRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<WorkFile> GetById(string id)
        {
            var file = await _set.Where(q => q.Id == id).Include(q => q.Work).SingleAsync();

            if (file is null)
            {
                throw new ObjectNotFoundException("File not found");
            }

            return file;

        }

        public async Task<List<WorkFile>> GetByWorkId(string workId)
        {
            var files = await _set.Where(q => q.WorkId == workId).Include(q => q.Work).ToListAsync();
            return files;
        }

        public async Task Delete(string fileId)
        {
            var entity = await GetById(fileId);
            _set.Remove(entity);
        }

    }
}
