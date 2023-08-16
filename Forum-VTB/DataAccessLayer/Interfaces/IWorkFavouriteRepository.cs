using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IWorkFavouriteRepository : IRepository<WorkFavourite>
    {
        Task<WorkFavourite> GetByWorkAndUserIds(string workId, string userId);

        Task Delete(string workId, string userId);

        Task<List<Work>> GetByUserId(string userId);

    }
}
