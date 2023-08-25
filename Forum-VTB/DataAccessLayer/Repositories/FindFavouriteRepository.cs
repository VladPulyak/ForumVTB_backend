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
    public class FindFavouriteRepository : Repository<FindFavourite>, IFindFavouriteRepository
    {
        public FindFavouriteRepository(ForumVTBDbContext context) : base(context)
        {
            
        }

        public async Task<FindFavourite> GetByFindAndUserIds(string findId, string userId)
        {
            var favourite = await _set.Where(q => q.FindId == findId && q.UserId == userId)
                .SingleOrDefaultAsync();
            return favourite;
        }

        public async Task Delete(string findId, string userId)
        {
            var favourite = await GetByFindAndUserIds(findId, userId);
            if (favourite is null)
            {
                throw new ObjectNotFoundException("Object with this findId or userId is not found");
            }
            _set.Remove(favourite);
        }

        public async Task<List<Find>> GetByUserId(string userId)
        {
            var finds = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Find.Files)
                .Include(q => q.Find.Subsection)
                .Include(q => q.Find.Subsection.Section)
                .Select(q => q.Find)
                .ToListAsync();
            return finds;
        }

    }
}
