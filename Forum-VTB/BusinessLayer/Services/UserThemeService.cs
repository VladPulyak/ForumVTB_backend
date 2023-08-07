using BusinessLayer.Dtos.UserThemes;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserThemeService : IUserThemeService
    {
        private readonly IUserThemeRepository _userThemeRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;

        public UserThemeService(IUserThemeRepository userThemeRepository, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager)
        {
            _userThemeRepository = userThemeRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task ChangeTheme(UserThemeDto userThemeDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userTheme = await _userThemeRepository.GetByUserId(user.Id);
            userTheme.Theme = userThemeDto.Theme;
            _userThemeRepository.Update(userTheme);
            await _userThemeRepository.Save();
        }

        public async Task<UserTheme> AddUserTheme(AddUserThemeDto userThemeDto)
        {
            var addedUserTheme = await _userThemeRepository.Add(new UserTheme
            {
                Id = Guid.NewGuid().ToString(),
                Theme = userThemeDto.Theme,
                UserId = userThemeDto.UserId
            });
            await _userThemeRepository.Save();
            return addedUserTheme;
        }

        public async Task<UserTheme> GetByUserId(string userId)
        {
            return await _userThemeRepository.GetByUserId(userId);
        }
    }
}
