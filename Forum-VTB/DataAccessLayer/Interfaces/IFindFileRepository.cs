using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFindFileRepository : IRepository<FindFile>
    {
        Task<FindFile> GetById(string id);

        Task<List<FindFile>> GetByFindId(string findId);

        Task Delete(string fileId);

        Task DeleteRange(string findId);
    }
}
