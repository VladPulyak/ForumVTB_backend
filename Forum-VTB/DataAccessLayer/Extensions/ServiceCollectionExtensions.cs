using DataAccessLayer.InfoModels;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection AddForumVTBDbContext(this IServiceCollection services, string connectionString, string vehiclesConnectionString)
        {
            services.AddDbContext<ForumVTBDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddDbContext<VehiclesDbContext>(options =>
            {
                options.UseNpgsql(vehiclesConnectionString);
            });
            return services;
        }

        private static IServiceCollection AddIdentityDependencies(this IServiceCollection services)
        {
            services.AddIdentityCore<UserProfile>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                    options.User.AllowedUserNameCharacters = "!@#$%^&*()_+-={}[]\\|:;\"'<>,.?/abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789";
                })
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<UserProfile>>("Forum-VTB.LoginProvider")
                .AddEntityFrameworkStores<ForumVTBDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddEntityDependencies(this IServiceCollection services, string connectionString, string vehiclesConnectionString)
        {
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IAdvertRepository, AdvertRepository>();
            services.AddScoped<ISubsectionRepository, SubsectionRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IAdvertCommentRepository, AdvertCommentRepository>();
            services.AddScoped<IUserMessageRepository, UserMessageRepository>();
            services.AddScoped<IRepository<MessageFile>, MessageFileRepository>();
            services.AddScoped<IRepository<Event>, EventRepository>();
            services.AddScoped<IUserChatRepository, UserChatRepository>();
            services.AddScoped<IUserThemeRepository, UserThemeRepository>();
            services.AddScoped<IAdvertFavouriteRepository, AdvertFavouriteRepository>();
            services.AddScoped<IAdvertFileRepository, AdvertFileRepository>();
            services.AddScoped<IWorkRepository, WorkRepository>();
            services.AddScoped<IWorkFileRepository, WorkFileRepository>();
            services.AddScoped<IWorkFavouriteRepository, WorkFavouriteRepository>();
            services.AddScoped<IReadOnlyRepository<AgriculturalMachineryInfo>, AgriculturalMachineryInfoRepository>();
            services.AddScoped<IReadOnlyRepository<BusesInfo>, BusesInfoRepository>();
            services.AddScoped<IReadOnlyRepository<CarsInfo>, CarsInfoRepository>();
            services.AddScoped<IReadOnlyRepository<LorriesInfo>, LorriesInfoRepository>();
            services.AddScoped<IReadOnlyRepository<MotorbikesInfo>, MotorbikesInfoRepository>();
            services.AddScoped<IReadOnlyRepository<ScootersInfo>, ScootersInfoRepository>();
            services.AddScoped<IReadOnlyRepository<SnowmobilesInfo>, SnowmobilesInfoRepository>();
            services.AddScoped<IReadOnlyRepository<TrailersInfo>, TrailersInfoRepository>();
            services.AddForumVTBDbContext(connectionString, vehiclesConnectionString);
            services.AddIdentityDependencies();
            return services;
        }
    }
}
