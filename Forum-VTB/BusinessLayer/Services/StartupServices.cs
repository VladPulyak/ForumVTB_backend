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
                await ChaptersSeedData(dbContext);
                await SectionsSeedDataAsync(dbContext);
                await TransportSubsectionsSeedDataAsync(dbContext);
                await ElectronicsSubsectionsSeedDataAsync(dbContext);
                await SportsAndRelaxSubsectionsSeedDataAsync(dbContext);
                await AnimalsSubsectionsSeedDataAsync(dbContext);
                await RealEstateSubsectionsSeedDataAsync(dbContext);
                await ClothesAndPersonalThingsSubsectionsSeedDataAsync(dbContext);
                await HouseAndGardenSubsectionsSeedDataAsync(dbContext);
                await ISuggestSubsectionsSeedDataAsync(dbContext);
                await ISearchSubsectionsSeedDataAsync(dbContext);
                await FindsSubsectionsSeedData(dbContext);
            }
        }

        private static async Task ChaptersSeedData(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Chapters.AnyAsync())
            {
                await dbContext.Chapters.AddRangeAsync(new List<Chapter>
                {
                    new Chapter {Name = "Buy-Sell"},
                    new Chapter {Name = "Services"},
                    new Chapter {Name = "Finds"},
                    new Chapter {Name = "Events"},
                    new Chapter {Name = "Forum"}
                });

                await dbContext.SaveChangesAsync();
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
                    new Section {Name = "Transport", ChapterId = 1},
                    new Section {Name = "Electronics", ChapterId = 1},
                    new Section {Name = "Sports and relax", ChapterId = 1},
                    new Section {Name = "Animals", ChapterId = 1},
                    new Section {Name = "Real estate", ChapterId = 1},
                    new Section {Name = "Clothes and personal things", ChapterId = 1},
                    new Section {Name = "House and garden", ChapterId = 1},
                    new Section {Name = "I suggest", ChapterId = 2},
                    new Section {Name = "I search", ChapterId = 2},
                });

                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Sections.AnyAsync(q => q.ChapterId == 3))
            {
                await dbContext.Sections.AddRangeAsync(new List<Section>
                {
                    new Section { Name = "Finds", ChapterId = 3 }
                });
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task FindsSubsectionsSeedData(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 10))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Losses", SectionId = 10},
                    new Subsection {Name = "Finds", SectionId = 10}
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task ISuggestSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 8))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Work", SectionId = 8},
                    new Subsection {Name = "Domestic", SectionId = 8},
                    new Subsection {Name = "Electronics", SectionId = 8},
                    new Subsection {Name = "Beauty and health", SectionId = 8},
                    new Subsection {Name = "Educational", SectionId = 8},
                    new Subsection {Name = "Transportation", SectionId = 8},
                    new Subsection {Name = "Advertising", SectionId = 8},
                    new Subsection {Name = "Buildings", SectionId = 8},
                    new Subsection {Name = "Animal", SectionId = 8},
                    new Subsection {Name = "Photo and video", SectionId = 8},
                    new Subsection {Name = "Legal", SectionId = 8},
                    new Subsection {Name = "Other", SectionId = 8},
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task ISearchSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 9))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Work", SectionId = 9},
                    new Subsection {Name = "Domestic", SectionId = 9},
                    new Subsection {Name = "Electronics", SectionId = 9},
                    new Subsection {Name = "Beauty and health", SectionId = 9},
                    new Subsection {Name = "Educational", SectionId = 9},
                    new Subsection {Name = "Transportation", SectionId = 9},
                    new Subsection {Name = "Advertising", SectionId = 9},
                    new Subsection {Name = "Buildings", SectionId = 9},
                    new Subsection {Name = "Animal", SectionId = 9},
                    new Subsection {Name = "Photo and video", SectionId = 9},
                    new Subsection {Name = "Legal", SectionId = 9},
                    new Subsection {Name = "Other", SectionId = 9},
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task TransportSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 1))
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

        private static async Task ElectronicsSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 2))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Phones and accessories", SectionId = 2},
                    new Subsection {Name = "Computers and accessories", SectionId = 2},
                    new Subsection {Name = "Audio and video", SectionId = 2},
                    new Subsection {Name = "Laptops", SectionId = 2},
                    new Subsection {Name = "Phototechnics", SectionId = 2},
                    new Subsection {Name = "Tablets, e-books", SectionId = 2},
                    new Subsection {Name = "Office equipment", SectionId = 2}
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SportsAndRelaxSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 3))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Bicycles and accessories", SectionId = 3},
                    new Subsection {Name = "Hunting and fishing", SectionId = 3},
                    new Subsection {Name = "Tourism", SectionId = 3},
                    new Subsection {Name = "Sport", SectionId = 3}
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task AnimalsSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 4))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Dogs", SectionId = 4},
                    new Subsection {Name = "Cats", SectionId = 4},
                    new Subsection {Name = "Birds", SectionId = 4},
                    new Subsection {Name = "Aquarium", SectionId = 4},
                    new Subsection {Name = "Other", SectionId = 4}
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task RealEstateSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 5))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Selling", SectionId = 5},
                    new Subsection {Name = "Daily rent", SectionId = 5},
                    new Subsection {Name = "Long-term rental", SectionId = 5},
                    new Subsection {Name = "Commercial estate", SectionId = 5}
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task ClothesAndPersonalThingsSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 6))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Clothing, shoes, accessories", SectionId = 6},
                    new Subsection {Name = "Products for children", SectionId = 6},
                    new Subsection {Name = "Beauty and health", SectionId = 6}
                });

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task HouseAndGardenSubsectionsSeedDataAsync(ForumVTBDbContext dbContext)
        {
            if (!await dbContext.Subsections.AnyAsync(q => q.SectionId == 7))
            {
                await dbContext.Subsections.AddRangeAsync(new List<Subsection>
                {
                    new Subsection {Name = "Repair and construction", SectionId = 7},
                    new Subsection {Name = "Furniture and interior", SectionId = 7},
                    new Subsection {Name = "Appliances", SectionId = 7},
                    new Subsection {Name = "Plants", SectionId = 7},
                    new Subsection {Name = "Cookware and kitchen goods", SectionId = 7}
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
