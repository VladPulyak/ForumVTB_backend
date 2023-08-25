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
    public class FindFileRepository : Repository<FindFile>, IFindFileRepository
    {
        public FindFileRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<FindFile> GetById(string id)
        {
            var file = await _set.Where(q => q.Id == id).Include(q => q.Find).SingleAsync();

            if (file is null)
            {
                throw new ObjectNotFoundException("File not found");
            }

            return file;

        }

        public async Task<List<FindFile>> GetByFindId(string findId)
        {
            var files = await _set.Where(q => q.FindId == findId).Include(q => q.Find).ToListAsync();
            return files;
        }

        public async Task Delete(string fileId)
        {
            var entity = await GetById(fileId);
            _set.Remove(entity);
        }

    }
}
