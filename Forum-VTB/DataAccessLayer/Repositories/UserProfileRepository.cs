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
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ForumVTBDbContext context) : base(context)
        {
        }

        public async Task<UserProfile> GetById(string id)
        {
            var user = await _set.SingleOrDefaultAsync(q => q.Id == id);
            return user;
        }
    }
}
