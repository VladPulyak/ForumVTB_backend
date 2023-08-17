using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IWorkFileRepository : IRepository<WorkFile>
    {
        Task<WorkFile> GetById(string id);

        Task<List<WorkFile>> GetByWorkId(string workId);

        Task Delete(string fileId);
    }
}
