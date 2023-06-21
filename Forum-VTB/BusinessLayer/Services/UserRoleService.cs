using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<UserRole> _userRoleRepository;

        public UserRoleService(IRepository<UserRole> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public Task<UserRole> Add(UserRole userRole)
        {
            return _userRoleRepository.Add(userRole);
        }

        public Task Delete(int id)
        {
            return _userRoleRepository.Delete(id);
        }

        public IQueryable<UserRole> GetAll()
        {
            return _userRoleRepository.GetAll();
        }

        public Task<UserRole> GetById(int id)
        {
            return _userRoleRepository.GetById(id);
        }

        public Task<UserRole> GetByName(string name)
        {
            return _userRoleRepository.GetAll().SingleAsync(q=>q.Name == name);             
        }

        public Task Save()
        {
            return _userRoleRepository.Save();
        }

        public UserRole Update(UserRole userRole)
        {
            return _userRoleRepository.Update(userRole);
        }
    }
}
