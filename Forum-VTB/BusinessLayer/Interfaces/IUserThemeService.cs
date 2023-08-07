using BusinessLayer.Dtos.UserThemes;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserThemeService
    {
        Task ChangeTheme(UserThemeDto requestDto);

        Task<UserTheme> AddUserTheme(AddUserThemeDto userThemeDto);

        Task<UserTheme> GetByUserId(string userId);
    }
}
