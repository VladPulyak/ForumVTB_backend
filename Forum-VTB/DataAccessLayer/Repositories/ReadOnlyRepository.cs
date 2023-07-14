using DataAccessLayer.Exceptions;
using DataAccessLayer.InfoModels;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        private readonly VehiclesDbContext _context;
        protected readonly DbSet<TEntity> _set;

        public ReadOnlyRepository(VehiclesDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }
        public IQueryable<TEntity> GetAll()
        {
            return _set.AsNoTracking();
        }

        public abstract Task<IEnumerable<TEntity>> GetByBrand(string brand);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
