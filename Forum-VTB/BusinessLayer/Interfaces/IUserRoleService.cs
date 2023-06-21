using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserRoleService
    {
        IQueryable<UserRole> GetAll();

        Task<UserRole> Add(UserRole userRole);

        Task<UserRole> GetById(int id);

        UserRole Update(UserRole userRole);

        Task<UserRole> GetByName(string name);

        Task Delete(int id);

        Task Save();
    }
}
