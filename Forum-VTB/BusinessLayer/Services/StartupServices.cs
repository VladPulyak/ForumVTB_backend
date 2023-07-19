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
                //await TopicsSeedDataAsync(dbContext);
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

        //private static async Task TopicsSeedDataAsync(ForumVTBDbContext dbContext)
        //{
        //    if (!await dbContext.Topics.AnyAsync())
        //    {
        //        await dbContext.Topics.AddRangeAsync(new List<Advert>
        //        {
        //            new Advert { Name = "Transport" },
        //            new Advert { Name = "Electronics" },
        //            new Advert { Name = "Sports and relax" },
        //            new Advert { Name = "Animals" },
        //            new Advert { Name = "Read estate" },
        //            new Advert { Name = "Clothes" },
        //            new Advert { Name = "Home and garden" }
        //        });

        //        await dbContext.SaveChangesAsync();
        //    }
        //}

        public static async Task PrintConnectionString(string connectionString)
        {
            await Console.Out.WriteLineAsync(connectionString);
        }
    }
}
