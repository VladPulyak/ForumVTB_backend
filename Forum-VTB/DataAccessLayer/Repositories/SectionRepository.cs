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
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(ForumVTBDbContext context) : base(context)
        {
        }

        public async Task<Section> GetByName(string name)
        {
            var entity = await _set.Include(q => q.Subsections).SingleOrDefaultAsync(q => q.Name == name);

            if (entity is null)
            {
                throw new ObjectNotFoundException("Section is not found");
            }

            return entity;
        }

    }
}
