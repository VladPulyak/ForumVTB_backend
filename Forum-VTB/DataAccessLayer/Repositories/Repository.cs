using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly ForumVTBDbContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(ForumVTBDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var newEntity = await _set.AddAsync(entity);
            return newEntity.Entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set.AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Invalid id");
            }

            var entity = await _set.FindAsync(id);

            if (entity is null)
            {
                throw new ObjectNotFoundException("Object not found");
            }

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = _set.Update(entity);
            return updatedEntity.Entity;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _set.Remove(entity);
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
