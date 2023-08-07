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
    public class UserThemeRepository : Repository<UserTheme>, IUserThemeRepository
    {
        public UserThemeRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<UserTheme> GetByUserId(string userId)
        {
            var userTheme = await _set.Where(q => q.UserId == userId).Include(q => q.User).SingleAsync();
            return userTheme;
        }
    }
}
