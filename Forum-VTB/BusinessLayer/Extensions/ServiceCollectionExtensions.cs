using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddEntityDependencies(connectionString);
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddAutoMapper(Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
