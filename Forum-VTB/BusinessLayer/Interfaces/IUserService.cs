using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        IQueryable<UserProfile> GetAll();

        Task Add(UserProfile userProfile);

        Task<UserProfile> GetById(int id);

        UserProfile Update(UserProfile userProfile);

        Task Delete(int id);

        Task<UserProfile> GetByLogin(string login);

        Task Save();
    }
}
