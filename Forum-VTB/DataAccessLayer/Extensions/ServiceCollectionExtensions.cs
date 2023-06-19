using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
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
        public static IServiceCollection AddForumVTBDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ForumVTBDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<UserProfile>, UserProfileRepository>();
            services.AddScoped<IRepository<UserRole>, UserRoleRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<Subsection>, SubsectionRepository>();
            services.AddScoped<IRepository<Section>, SectionRepository>();
            services.AddScoped<IRepository<Message>, MessageRepository>();
            services.AddScoped<IRepository<MessageFile>, MessageFileRepository>();
            return services;
        }
    }
}
