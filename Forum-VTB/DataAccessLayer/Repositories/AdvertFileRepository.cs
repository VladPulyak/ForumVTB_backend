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
    public class AdvertFileRepository : Repository<AdvertFile>, IAdvertFileRepository
    {
        private readonly ForumVTBDbContext _dbContext;

        public AdvertFileRepository(ForumVTBDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<AdvertFile> GetById(string id)
        {
            var file = await _set.Where(q => q.Id == id).Include(q => q.Advert).SingleAsync();

            if (file is null)
            {
                throw new ObjectNotFoundException("File not found");
            }

            return file;

        }

        public void AddRange(IEnumerable<AdvertFile> advertFiles)
        {
            _set.AddRangeAsync(advertFiles);
        }

        public async Task<List<AdvertFile>> GetByAdvertId(string advertId)
        {
            var files = await _set.Where(q => q.AdvertId == advertId).Include(q => q.Advert).ToListAsync();
            return files;
        }

        public async Task Delete(string fileId)
        {
            var entity = await GetById(fileId);
            _set.Remove(entity);
        }
    }
}
