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

        public override IQueryable<Advert> GetAll()
        {
            return _set.Where(q => q.Status == "Active")
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Include(q => q.Favourites)
                .Include(q => q.User)
                .AsNoTracking();
        }

        public async Task Delete(string advertId)
        {
            var entity = await GetById(advertId);
            _set.Remove(entity);
        }

        public async Task<List<Advert>> GetByUserId(string userId)
        {
            var adverts = await _set.Where(q => q.UserId == userId)
                .Include(q => q.AdvertComments)
                .Include(q => q.Files)
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .OrderBy(q => q.DateOfCreation)
                .ToListAsync();
            return adverts;
        }

        public async Task<Advert> GetActiveById(string advertId)
        {
            var advert = await _set.Where(q => q.Id == advertId && q.Status == "Active")
                .Include(q => q.User)
                .Include(q => q.AdvertComments)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Include(q => q.Files)
                .SingleAsync();
            if (advert is null)
            {
                throw new ObjectNotFoundException("Adverts with this id is not found");
            }

            return advert;
        }

        public async Task<Advert> GetById(string advertId)
        {
            var advert = await _set.Where(q => q.Id == advertId)
                .Include(q => q.User)
                .Include(q => q.AdvertComments)
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
                .Where(q => q.Subsection.Section.Name == sectionName && q.Status == "Active")
                .Include(q => q.User)
                .ToListAsync();

            return entity;
        }

        public async Task<List<Advert>> GetBySubsectionName(string subsectionName, string sectionName)
        {
            var entity = await _set
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Name == subsectionName && q.Status == "Active" && q.Subsection.Section.Name == sectionName)
                .Include(q => q.Files)
                .Include(q => q.User)
                .ToListAsync();

            return entity;
        }

        public async Task<List<Advert>> SearchByKeyPhrase(string keyPhrase)
        {
            keyPhrase = keyPhrase.Trim();
            return await _set.Where(a => (a.Title.ToUpper().Contains(keyPhrase.ToUpper()) || a.Description.ToUpper().Contains(keyPhrase.ToUpper())) && a.Status == "Active")
                .Include(q => q.Files)
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .ToListAsync();
        }
    }
}
