using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
        Task<List<Topic>> GetByUserId(string topicId);

        Task Delete(string topicId);

        Task<Topic> GetById(string topicId);

        Task<List<Topic>> GetBySectionName(string sectionName);

        Task<List<Topic>> GetBySubsectionName(string subsectionName, string sectionName);

        Task<List<Topic>> SearchByKeyPhrase(string keyPhrase);
    }
}
