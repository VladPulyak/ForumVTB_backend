using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserMessageRepository : IRepository<UserMessage>
    {
        Task<List<UserMessage>> GetByReceiverId(string id);

        Task<List<UserMessage>> GetBySenderId(string id);

        Task<UserMessage> GetByDateOfCreation(DateTime dateOfCreation, string userId);

        Task Delete(DateTime dateOfCreation, string userId);
    }
}
