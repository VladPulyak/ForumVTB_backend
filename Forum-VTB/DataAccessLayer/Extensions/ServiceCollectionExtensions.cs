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
        private static IServiceCollection AddForumVTBDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ForumVTBDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            return services;
        }

        private static IServiceCollection AddIdentityDependencies(this IServiceCollection services)
        {
            services.AddIdentityCore<UserProfile>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                })
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<UserProfile>>("Forum-VTB.LoginProvider")
                .AddEntityFrameworkStores<ForumVTBDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddEntityDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IRepository<UserProfile>, UserProfileRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<Subsection>, SubsectionRepository>();
            services.AddScoped<IRepository<Section>, SectionRepository>();
            services.AddScoped<IRepository<Message>, MessageRepository>();
            services.AddScoped<IRepository<MessageFile>, MessageFileRepository>();
            services.AddForumVTBDbContext(connectionString);
            services.AddIdentityDependencies();
            return services;
        }
    }
}
