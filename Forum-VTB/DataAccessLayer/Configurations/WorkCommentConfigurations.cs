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
    public class WorkCommentConfigurations : IEntityTypeConfiguration<WorkComment>
    {
        public void Configure(EntityTypeBuilder<WorkComment> builder)
        {
            builder.ToTable("WorkComments");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.Text).IsRequired();
            builder.Property(q => q.Text).HasMaxLength(300);
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp with time zone").IsRequired();
            builder.HasOne(q => q.UserProfile).WithMany(w => w.WorkComments).HasForeignKey(q => q.UserId);
            builder.HasOne(q => q.Work).WithMany(w => w.Comments).HasForeignKey(q => q.WorkId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.ParentComment).WithMany(q => q.Replies).HasForeignKey(q => q.ParentCommentId);
        }
    }
}
