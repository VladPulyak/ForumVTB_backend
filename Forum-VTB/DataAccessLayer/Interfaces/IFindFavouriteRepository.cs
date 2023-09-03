using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFindFavouriteRepository : IRepository<FindFavourite>
    {
        Task<FindFavourite> GetByFindAndUserIds(string findId, string userId);

        Task Delete(string findId, string userId);

        Task<List<Find>> GetByUserId(string userId);
    }
}
