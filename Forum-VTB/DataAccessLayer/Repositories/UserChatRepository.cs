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
    public class UserChatRepository : Repository<UserChat>, IUserChatRepository
    {
        public UserChatRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<UserChat> GetByUserIds(string firstUserId, string secondUserId)
        {
            return await _set.Where(q => q.FirstUserId == firstUserId && q.SecondUserId == secondUserId || q.FirstUserId == secondUserId && q.SecondUserId == firstUserId).SingleOrDefaultAsync();
        }

        public async Task<UserChat> GetByUserIdsAndAdvertId(string firstUserId, string secondUserId, string advertId)
        {
            return await _set.Where(q => (q.FirstUserId == firstUserId && q.SecondUserId == secondUserId || q.FirstUserId == secondUserId && q.SecondUserId == firstUserId) && q.AdvertId == advertId).SingleOrDefaultAsync();
        }

        public async Task<List<UserChat>> GetByUserId(string userId)
        {
            return await _set.Where(q => q.FirstUserId == userId || q.SecondUserId == userId)
                .Include(q => q.FirstUser)
                .Include(q => q.SecondUser)
                .ToListAsync();
        }
    }
}