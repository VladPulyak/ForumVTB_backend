﻿using DataAccessLayer.Exceptions;
using DataAccessLayer.InfoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MotorbikesInfoRepository : ReadOnlyRepository<MotorbikesInfo>
    {
        public MotorbikesInfoRepository(VehiclesDbContext context) : base(context)
        {
            
        }

        public async override Task<IEnumerable<MotorbikesInfo>> GetByBrand(string brand)
        {
            if (string.IsNullOrEmpty(brand))
            {
                throw new InvalidArgumentException("Invalid argument");
            }

            var vehicles = _set.Where(q => q.Brand == brand);

            if (!vehicles.Any())
            {
                throw new ObjectNotFoundException("This brand is not found");
            }

            return await vehicles.ToListAsync();
        }

    }
}
