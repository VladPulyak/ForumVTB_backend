using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class TopicFavouriteConfigurations : IEntityTypeConfiguration<TopicFavourite>
    {
        public void Configure(EntityTypeBuilder<TopicFavourite> builder)
        {
            builder.ToTable("TopicFavourites");
            builder.HasKey(q => q.Id);
            builder.HasOne(q => q.User).WithMany(w => w.TopicFavourites).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.Topic).WithMany(w => w.Favourites).HasForeignKey(q => q.TopicId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(q => new
            {
                q.UserId,
                q.TopicId
            }).IsUnique();

        }
    }
}
