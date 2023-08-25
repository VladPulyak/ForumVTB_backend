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
    public class FindRepository : Repository<Find>, IFindRepository
    {
        public FindRepository(ForumVTBDbContext context) : base(context)
        {
        }

        public override IQueryable<Find> GetAll()
        {
            return _set.Where(q => q.Status == "Active")
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Include(q => q.Favourites)
                .AsNoTracking();
        }

        public async Task Delete(string findId)
        {
            var find = await GetById(findId);
            _set.Remove(find);
        }

        public async Task<List<Find>> GetByUserId(string userId)
        {
            var adverts = await _set.Where(q => q.UserId == userId)
                .Include(q => q.FindComments)
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .OrderBy(q => q.DateOfCreation)
                .ToListAsync();
            return adverts;
        }

        public async Task<Find> GetActiveById(string findId)
        {
            var find = await _set.Where(q => q.Id == findId && q.Status == "Active")
                .Include(q => q.User)
                .Include(q => q.FindComments)
                .Include(q => q.Subsection)
                .Include(q => q.Files)
                .SingleAsync();
            if (find is null)
            {
                throw new ObjectNotFoundException("Find with this id is not found");
            }

            return find;
        }

        public async Task<Find> GetById(string findId)
        {
            var find = await _set.Where(q => q.Id == findId)
                .Include(q => q.User)
                .Include(q => q.FindComments)
                .Include(q => q.Subsection)
                .Include(q => q.Files)
                .SingleAsync();
            if (find is null)
            {
                throw new ObjectNotFoundException("Find with this id is not found");
            }

            return find;

        }

        public async Task<List<Find>> GetBySectionName(string sectionName)
        {
            var finds = await _set.Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Section.Name == sectionName && q.Status == "Active")
                .ToListAsync();

            return finds;
        }

        public async Task<List<Find>> GetBySubsectionName(string subsectionName, string sectionName)
        {
            var finds = await _set
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Name == subsectionName && q.Status == "Active" && q.Subsection.Section.Name == sectionName)
                .Include(q => q.Files)
                .ToListAsync();

            return finds;
        }

        public async Task<List<Find>> SearchByKeyPhrase(string keyPhrase)
        {
            keyPhrase = keyPhrase.Trim();
            return await _set.Where(a => (a.Title.ToUpper().Contains(keyPhrase.ToUpper()) || a.Description.ToUpper().Contains(keyPhrase.ToUpper())) && a.Status == "Active")
                .Include(q => q.Files)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .ToListAsync();
        }

    }
}
