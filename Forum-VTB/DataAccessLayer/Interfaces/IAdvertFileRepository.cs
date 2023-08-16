using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAdvertFileRepository : IRepository<AdvertFile>
    {
        Task<AdvertFile> GetById(string id);

        Task<List<AdvertFile>> GetByAdvertId(string advertId);

        Task Delete(string fileId);
    }
}
