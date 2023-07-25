using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<List<Advert>> GetByUserId(string userId);

        //Task Delete(DateTime dateOfCreation, string userId);

        Task Delete(string advertId);

        //Task<Advert> GetByDateOfCreationForUser(DateTime dateOfCreation, string userId);

        Task<Advert> GetById(string advertId);
    }
}
