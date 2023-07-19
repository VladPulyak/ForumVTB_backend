using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Services
{
    public static class StartupServices
    {
        public static async Task MigrateDatabase(IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ForumVTBDbContext>();
                dbContext.Database.Migrate();
                await RolesSeedDataAsync(dbContext);
            }
        }

        private static async Task RolesSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Roles.AnyAsync())
            {
                await dbContext.Roles.AddRangeAsync(new List<IdentityRole>
                {
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Name = "User", NormalizedName = "USER" }
                });

                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task PrintConnectionString(string connectionString)
        {
            await Console.Out.WriteLineAsync(connectionString);
        }
    }
}
