using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITopicMessageRepository : IRepository<TopicMessage>
    {
        Task<TopicMessage> GetById(string id);

        Task<List<TopicMessage>> GetByTopicId(string topicId);

        Task Delete(string messageId);
    }
}
