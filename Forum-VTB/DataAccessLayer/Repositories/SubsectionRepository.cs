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
    public class SubsectionRepository : Repository<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public async Task<Subsection> GetBySubsectionAndSectionNames(string sectionName, string subsectionName)
        {
            var entity = await _set
                .Where(q => q.Name == subsectionName)
                .Include(q => q.Section)
                .SingleOrDefaultAsync(q => q.Section.Name == sectionName);

            if (entity is null)
            {
                throw new ObjectNotFoundException("Subsection is not found");
            }

            return entity;
        }

        public async Task<List<Subsection>> GetBySectionName(string sectionName)
        {
            var entity = await _set.Include(q => q.Adverts)
                .Include(q => q.Section)
                .Where(q => q.Section.Name == sectionName)
                .ToListAsync();

            if (entity is null)
            {
                throw new ObjectNotFoundException("Subsections with this section name is not found");
            }

            return entity;
        }
    }
}
