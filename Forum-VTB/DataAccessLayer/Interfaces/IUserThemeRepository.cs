using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserThemeRepository : IRepository<UserTheme>
    {
        Task<UserTheme> GetByUserId(string userId);
    }
}
