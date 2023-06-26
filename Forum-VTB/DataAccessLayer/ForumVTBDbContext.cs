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
            base.OnModelCreating(modelBuilder);
        }
    }
}
