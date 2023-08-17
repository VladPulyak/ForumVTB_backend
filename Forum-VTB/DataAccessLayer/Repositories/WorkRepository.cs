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
    public class WorkRepository : Repository<Work>, IWorkRepository
    {
        public WorkRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public override IQueryable<Work> GetAll()
        {
            return _set.Where(q => q.Status == "Active")
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Favourites)
                .AsNoTracking();
        }

        public async Task Delete(string workId)
        {
            var entity = await GetById(workId);
            _set.Remove(entity);
        }

        public async Task<List<Work>> GetByUserId(string userId)
        {
            var adverts = await _set.Where(q => q.UserId == userId)
                .Include(q => q.Files)
                .OrderBy(q => q.DateOfCreation)
                .ToListAsync();
            return adverts;
        }

        public async Task<Work> GetById(string workId)
        {
            var advert = await _set.Where(q => q.Id == workId && q.Status == "Active")
                .Include(q => q.User)
                .Include(q => q.Subsection)
                .Include(q => q.Files)
                .SingleAsync();
            if (advert is null)
            {
                throw new ObjectNotFoundException("Works with this id is not found");
            }

            return advert;
        }

        public async Task<List<Work>> GetBySectionName(string sectionName)
        {
            var entity = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Section.Name == sectionName && q.Status == "Active")
                .ToListAsync();

            return entity;
        }

        public async Task<List<Work>> GetBySubsectionName(string subsectionName)
        {
            var entity = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Where(q => q.Subsection.Name == subsectionName && q.Status == "Active")
                .ToListAsync();

            return entity;
        }

        public async Task<List<Work>> SearchByKeyPhrase(string keyPhrase)
        {
            keyPhrase = keyPhrase.Trim();
            return await _set.Where(a => (a.Title.ToUpper().Contains(keyPhrase.ToUpper()) || a.Description.ToUpper().Contains(keyPhrase.ToUpper())) && a.Status == "Active")
                .Include(q => q.Files)
                .ToListAsync();
        }
    }
}
