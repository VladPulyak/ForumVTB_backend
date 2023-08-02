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
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        public AdvertRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public IQueryable<Advert> GetAdvertsWithSubsections()
        {
            return _set.Include(q => q.Subsection).AsNoTracking();
        }

        public async Task Delete(string advertId)
        {
            var entity = await GetById(advertId);
            _set.Remove(entity);
        }

        public async Task<List<Advert>> GetByUserId(string userId)
        {
            var adverts = await _set.Where(q => q.UserId == userId).Include(q => q.Comments).ToListAsync();
            if (!adverts.Any())
            {
                throw new ObjectNotFoundException("Adverts for this user is not found");
            }

            return adverts;
        }

        public async Task<Advert> GetById(string advertId)
        {
            var advert = await _set.Where(q => q.Id == advertId)
                .Include(q => q.User)
                .Include(q => q.Comments)
                .Include(q => q.Subsection)
                .Include(q => q.Files)
                .SingleAsync();
            if (advert is null)
            {
                throw new ObjectNotFoundException("Adverts with this id is not found");
            }

            return advert;
        }

        public async Task<List<Advert>> GetBySectionName(string sectionName)
        {
            var entity = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Section.Name == sectionName)
                .ToListAsync();

            return entity;
        }

        public async Task<List<Advert>> GetBySubsectionName(string subsectionName)
        {
            var entity = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Where(q => q.Subsection.Name == subsectionName)
                .ToListAsync();

            return entity;
        }

    }
}
