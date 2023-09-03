using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserChatRepository : IRepository<UserChat>
    {
        Task<UserChat> GetByUserIds(string firstUserId, string secondUserId);

        Task<List<UserChat>> GetByUserId(string userId);

        Task<UserChat> GetByUserIdsAndAdvertId(string firstUserId, string secondUserId, string advertId);
    }
}
