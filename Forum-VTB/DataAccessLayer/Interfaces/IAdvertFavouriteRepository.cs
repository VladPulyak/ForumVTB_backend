using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAdvertFavouriteRepository : IRepository<AdvertFavourite>
    {
        Task<AdvertFavourite> GetByAdvertAndUserIds(string advertId, string userId);

        Task Delete(string advertId, string userId);

        Task<List<Advert>> GetByUserId(string userId);
    }
}
