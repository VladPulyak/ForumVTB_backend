using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFindCommentRepository : IRepository<FindComment>
    {
        Task<FindComment> GetById(string id);

        Task<List<FindComment>> GetByFindId(string findId);

        Task Delete(string commentId);
    }
}
