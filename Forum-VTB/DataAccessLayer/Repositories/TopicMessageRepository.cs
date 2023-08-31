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
    public class TopicMessageRepository : Repository<TopicMessage>, ITopicMessageRepository
    {
        public TopicMessageRepository(ForumVTBDbContext context) : base(context)
        {
            
        }

        public async Task<TopicMessage> GetById(string id)
        {
            var message = await _set.Where(q => q.Id == id).Include(q => q.Topic).Include(q => q.UserProfile).SingleAsync();

            if (message is null)
            {
                throw new ObjectNotFoundException("Message not found");
            }

            return message;
        }

        public async Task Delete(string commentId)
        {
            var comment = await GetById(commentId);
            _set.Remove(comment);
        }

        public async Task<List<TopicMessage>> GetByTopicId(string topicId)
        {
            var messages = await _set.Where(q => q.TopicId == topicId && q.ParentMessageId == null)
                                     .Include(q => q.UserProfile)
                                     .Include(q => q.Replies)
                                     .ToListAsync();

            return messages;
        }
    }
}
