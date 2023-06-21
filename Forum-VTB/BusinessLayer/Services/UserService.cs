using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Exceptions;
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
    public class UserService : IUserService
    {
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly ForumVTBDbContext _context;

        public UserService(IRepository<UserProfile> userProfileRepository, ForumVTBDbContext context)
        {
            _userProfileRepository = userProfileRepository;
            _context = context;
        }

        public Task Add(UserProfile entity)
        {
            return _userProfileRepository.Add(entity);
        }

        public Task Delete(int id)
        {
            return _userProfileRepository.Delete(id);
        }

        public IQueryable<UserProfile> GetAll()
        {
            return _userProfileRepository.GetAll();
        }

        public Task<UserProfile> GetByLogin(string login)
        {
            var user = _context.UserProfiles.Include(q=>q.UserRole).SingleOrDefaultAsync(q=>q.Login == login);

            if (user is null)
            {
                throw new ObjectNotFoundException("User with this login is not found");
            }

            return user;
        }

        public Task<UserProfile> GetById(int id)
        {
            return _userProfileRepository.GetById(id);
        }

        public Task Save()
        {
            return _userProfileRepository.Save();
        }

        public UserProfile Update(UserProfile userProfile)
        {
            return _userProfileRepository.Update(userProfile);
        }
    }
}
