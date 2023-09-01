using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task Delete(string eventId);

        Task<Event> GetById(string eventId);

        Task<List<Event>> GetBySubsectionName(string subsectionName, string sectionName);

        Task<List<Event>> SearchByKeyPhrase(string keyPhrase);
    }
}
