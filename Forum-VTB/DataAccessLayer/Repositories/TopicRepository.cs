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
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public override IQueryable<Topic> GetAll()
        {
            return _set
                .Include(q => q.Files)
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Include(q => q.Favourites)
                .AsNoTracking();
        }

        public async Task Delete(string topicId)
        {
            var entity = await GetById(topicId);
            _set.Remove(entity);
        }

        public async Task<List<Topic>> GetByUserId(string userId)
        {
            var topics = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Messages)
                .Include(q => q.Files)
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .OrderBy(q => q.DateOfCreation)
                .ToListAsync();
            return topics;
        }

        public async Task<Topic> GetById(string topicId)
        {
            var topic = await _set.Where(q => q.Id == topicId)
                .Include(q => q.User)
                .Include(q => q.Messages)
                .Include(q => q.Subsection)
                .Include(q => q.Files)
                .SingleAsync();
            if (topic is null)
            {
                throw new ObjectNotFoundException("Topic with this id is not found");
            }

            return topic;

        }

        public async Task<List<Topic>> GetBySectionName(string sectionName)
        {
            var entity = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Section.Name == sectionName)
                .Include(q => q.Messages)
                .Include(q => q.User)
                .ToListAsync();

            return entity;
        }

        public async Task<List<Topic>> GetBySubsectionName(string subsectionName, string sectionName)
        {
            var entity = await _set
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Name == subsectionName && q.Subsection.Section.Name == sectionName)
                .Include(q => q.Files)
                .Include(q => q.Messages)
                .Include(q => q.User)
                .ToListAsync();

            return entity;
        }

        public async Task<List<Topic>> SearchByKeyPhrase(string keyPhrase)
        {
            keyPhrase = keyPhrase.Trim();
            return await _set.Where(a => (a.Title.ToUpper().Contains(keyPhrase.ToUpper()) || a.Description.ToUpper().Contains(keyPhrase.ToUpper())))
                .Include(q => q.Files)
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .ToListAsync();
        }
    }
}
