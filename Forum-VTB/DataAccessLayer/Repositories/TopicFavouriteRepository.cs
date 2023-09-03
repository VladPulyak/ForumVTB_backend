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
    public class TopicFavouriteRepository : Repository<TopicFavourite>, ITopicFavouriteRepository
    {
        public TopicFavouriteRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<TopicFavourite> GetByTopicAndUserIds(string topicId, string userId)
        {
            var favourite = await _set.Where(q => q.TopicId == topicId && q.UserId == userId)
                .SingleOrDefaultAsync();
            return favourite;
        }

        public async Task Delete(string topicId, string userId)
        {
            var favourite = await GetByTopicAndUserIds(topicId, userId);
            if (favourite is null)
            {
                throw new ObjectNotFoundException("Object with this topicId or userId is not found");
            }
            _set.Remove(favourite);
        }

        public async Task<List<Topic>> GetByUserId(string userId)
        {
            var topics = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Topic.Files)
                .Include(q => q.Topic.Subsection)
                .Include(q => q.Topic.Subsection.Section)
                .Select(q => q.Topic)
                .ToListAsync();
            return topics;
        }

    }
}
