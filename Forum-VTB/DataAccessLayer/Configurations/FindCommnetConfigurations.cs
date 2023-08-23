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
    public class FindCommnetConfigurations : IEntityTypeConfiguration<FindComment>
    {
        public void Configure(EntityTypeBuilder<FindComment> builder)
        {
            builder.ToTable("FindComments");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.Text).IsRequired();
            builder.Property(q => q.Text).HasMaxLength(300);
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp with time zone").IsRequired();
            builder.HasOne(q => q.UserProfile).WithMany(w => w.FindComments).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.Find).WithMany(w => w.FindComments).HasForeignKey(q => q.FindId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.ParentComment).WithMany(q => q.Replies).HasForeignKey(q => q.ParentCommentId);

        }
    }
}
