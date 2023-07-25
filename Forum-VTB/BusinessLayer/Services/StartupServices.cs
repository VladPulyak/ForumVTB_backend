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
                await SectionsSeedDataAsync(dbContext);
                await SubsectionsSeedDataAsync(dbContext);
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

        private static async Task SectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Sections.AnyAsync())
            {
                await dbContext.Sections.AddRangeAsync(new List<Section>
                {
                    new Section {Name = "Transport"},
                    new Section {Name = "Electronics"},
                    new Section {Name = "Sports and relax"},
                    new Section {Name = "Animals"},
                    new Section {Name = "Real estate"},
                    new Section {Name = "Clothes and personal things"},
                    new Section {Name = "House and garden"}
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync())
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Autos", SectionId = 1},
                    new Subsection {Name = "Motorbikes", SectionId = 1},
                    new Subsection {Name = "Quadbikes, snowbikes and water transport", SectionId = 1},
                    new Subsection {Name = "Lorries, buses", SectionId = 1},
                    new Subsection {Name = "Special equipment", SectionId = 1},
                    new Subsection {Name = "Accessories and spare parts", SectionId = 1}
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
