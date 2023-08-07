using DataAccessLayer.Configurations;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ForumVTBDbContext : IdentityDbContext<UserProfile>
    {
        public ForumVTBDbContext()
        {

        }

        public ForumVTBDbContext(DbContextOptions<ForumVTBDbContext> options) : base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<AdvertComment> TopicMessages { get; set; }

        public DbSet<MessageFile> Files { get; set; }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<AdvertFile> AdvertFiles { get; set; }

        public DbSet<Subsection> Subsections { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<UserMessage> UserMessages { get; set; }

        public DbSet<Favourite> Favourites { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfigurations());
            modelBuilder.ApplyConfiguration(new AdvertCommentConfigurations());
            modelBuilder.ApplyConfiguration(new MessageFileConfigurations());
            modelBuilder.ApplyConfiguration(new AdvertConfigurations());
            modelBuilder.ApplyConfiguration(new SubsectionConfigurations());
            modelBuilder.ApplyConfiguration(new SectionConfigurations());
            modelBuilder.ApplyConfiguration(new UserMessageConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertFileConfugiration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
