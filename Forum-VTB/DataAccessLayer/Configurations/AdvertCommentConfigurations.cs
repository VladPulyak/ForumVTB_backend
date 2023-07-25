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
    public class AdvertCommentConfigurations : IEntityTypeConfiguration<AdvertComment>
    {
        public void Configure(EntityTypeBuilder<AdvertComment> builder)
        {
            builder.ToTable("AdvertComments");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.Text).IsRequired();
            builder.Property(q => q.Text).HasMaxLength(300);
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp").IsRequired();
            builder.HasOne(q => q.UserProfile).WithMany(w => w.Comments).HasForeignKey(q => q.UserId);
            builder.HasOne(q => q.Advert).WithMany(w => w.Comments).HasForeignKey(q => q.AdvertId);
            builder.HasOne(q => q.ParentComment).WithMany(q => q.Replies).HasForeignKey(q => q.ParentCommentId);
        }
    }
}
