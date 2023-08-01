using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAdvertCommentRepository : IRepository<AdvertComment>
    {
        Task<AdvertComment> GetById(string id);

        Task Delete(string id);

        Task<List<AdvertComment>> GetByAdvertId(string advertId);

        Task<AdvertComment> GetByDateOfCreation(DateTime dateOfCreation, string userId);
    }
}