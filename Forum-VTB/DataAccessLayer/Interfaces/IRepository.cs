using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> GetById(int id);

        TEntity Update(TEntity entity);

        Task Delete(int id);

        Task Save();
    }
}
