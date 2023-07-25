﻿using DataAccessLayer.Exceptions;
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

        public async Task<Subsection> GetByName(string name)
        {
            var entity = await _set.SingleOrDefaultAsync(q => q.Name == name);

            if (entity is null)
            {
                throw new ObjectNotFoundException("Subsection is not found");
            }

            return entity;
        }
    }
}
