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

        public DbSet<AdvertComment> AdvertComments { get; set; }

        public DbSet<MessageFile> MessageFiles { get; set; }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<AdvertFile> AdvertFiles { get; set; }

        public DbSet<Subsection> Subsections { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<UserMessage> UserMessages { get; set; }

        public DbSet<AdvertFavourite> AdvertFavourites { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<UserTheme> Themes { get; set; }

        public DbSet<UserChat> UserChats { get; set; }

        public DbSet<Job> Works { get; set; }

        public DbSet<JobFavourite> WorkFavourites { get; set; }

        public DbSet<JobFile> WorkFiles { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Find> Finds { get; set; }

        public DbSet<FindComment> FindComments { get; set; }

        public DbSet<FindFavourite> FindFavourites { get; set; }

        public DbSet<FindFile> FindFiles { get; set; }

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
            modelBuilder.ApplyConfiguration(new AdvertFavouriteConfigurations());
            modelBuilder.ApplyConfiguration(new EventConfigurations());
            modelBuilder.ApplyConfiguration(new UserThemeConfigurations());
            modelBuilder.ApplyConfiguration(new UserChatConfigurations());
            modelBuilder.ApplyConfiguration(new JobConfigurations());
            modelBuilder.ApplyConfiguration(new JobFavouriteConfigurations());
            modelBuilder.ApplyConfiguration(new JobFileConfigurations());
            modelBuilder.ApplyConfiguration(new ChapterConfigurations());
            modelBuilder.ApplyConfiguration(new FindCommnetConfigurations());
            modelBuilder.ApplyConfiguration(new FindConfigurations());
            modelBuilder.ApplyConfiguration(new FindFavouriteConfigurations());
            modelBuilder.ApplyConfiguration(new FindFileConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
