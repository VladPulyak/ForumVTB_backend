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
    public class FavouriteRepository : Repository<Favourite>, IFavouriteRepository
    {
        public FavouriteRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<Favourite> GetByAdvertAndUserIds(string advertId, string userId)
        {
            var favourite = await _set.Where(q => q.AdvertId == advertId && q.UserId == userId)
                .SingleOrDefaultAsync();
            if (favourite is null)
            {
                throw new ObjectNotFoundException("Object with this advertId or userId is not found");
            }
            return favourite;
        }

        public async Task Delete(string advertId, string userId)
        {
            var favourite = await GetByAdvertAndUserIds(advertId, userId);
            _set.Remove(favourite);
        }

        public async Task<List<Advert>> GetByUserId(string userId)
        {
            var adverts = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Advert.Files)
                .Select(q => q.Advert)
                .ToListAsync();
            return adverts;
        }
    }
}
