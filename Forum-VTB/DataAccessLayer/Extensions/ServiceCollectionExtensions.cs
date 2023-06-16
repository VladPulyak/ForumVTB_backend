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
        public static void AddForumVTBDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ForumVTBDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
    }
}
