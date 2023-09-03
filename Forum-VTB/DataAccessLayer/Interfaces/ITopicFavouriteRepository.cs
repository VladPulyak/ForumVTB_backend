using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITopicFavouriteRepository : IRepository<TopicFavourite>
    {
        Task<TopicFavourite> GetByTopicAndUserIds(string topicId, string userId);

        Task Delete(string topicId, string userId);

        Task<List<Topic>> GetByUserId(string userId);
    }
}
