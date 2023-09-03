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
    public class TopicFileRepository : Repository<TopicFile>, ITopicFileRepository
    {
        public TopicFileRepository(ForumVTBDbContext context) : base(context)
        {
            
        }

        public async Task<TopicFile> GetById(string id)
        {
            var file = await _set.Where(q => q.Id == id).Include(q => q.Topic).SingleAsync();

            if (file is null)
            {
                throw new ObjectNotFoundException("File not found");
            }

            return file;

        }

        public async Task<List<TopicFile>> GetByTopicId(string topicId)
        {
            var files = await _set.Where(q => q.TopicId == topicId).Include(q => q.Topic).ToListAsync();
            return files;
        }

        public async Task Delete(string fileId)
        {
            var entity = await GetById(fileId);
            _set.Remove(entity);
        }

    }
}
