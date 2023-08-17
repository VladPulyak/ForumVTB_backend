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
    public class WorkFavouriteRepository : Repository<WorkFavourite>, IWorkFavouriteRepository
    {
        public WorkFavouriteRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<WorkFavourite> GetByWorkAndUserIds(string workId, string userId)
        {
            var favourite = await _set.Where(q => q.WorkId == workId && q.UserId == userId)
                .SingleOrDefaultAsync();
            return favourite;
        }

        public async Task Delete(string workId, string userId)
        {
            var favourite = await GetByWorkAndUserIds(workId, userId);
            if (favourite is null)
            {
                throw new ObjectNotFoundException("Object with this advertId or userId is not found");
            }
            _set.Remove(favourite);
        }

        public async Task<List<Work>> GetByUserId(string userId)
        {
            var adverts = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Work.Files)
                .Select(q => q.Work)
                .ToListAsync();
            return adverts;
        }

    }
}
