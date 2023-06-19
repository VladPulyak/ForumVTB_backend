using DataAccessLayer.Configurations;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ForumVTBDbContext : DbContext
    {
        public ForumVTBDbContext()
        {
            
        }

        public ForumVTBDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageFile> Files { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Subsection> Subsections { get; set; }

        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfigurations());
            modelBuilder.ApplyConfiguration(new MessageConfigurations());
            modelBuilder.ApplyConfiguration(new MessageFileConfigurations());
            modelBuilder.ApplyConfiguration(new TopicConfigurations());
            modelBuilder.ApplyConfiguration(new SubsectionConfigurations());
            modelBuilder.ApplyConfiguration(new SectionConfigurations());
            modelBuilder.ApplyConfiguration(new UserRoleConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
