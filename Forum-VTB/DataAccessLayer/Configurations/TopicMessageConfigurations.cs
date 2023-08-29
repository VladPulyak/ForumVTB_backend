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
    public class TopicMessageConfigurations : IEntityTypeConfiguration<TopicMessage>
    {
        public void Configure(EntityTypeBuilder<TopicMessage> builder)
        {
            builder.ToTable("TopicMessages");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.Text).IsRequired();
            builder.Property(q => q.Text).HasMaxLength(300);
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp with time zone").IsRequired();
            builder.HasOne(q => q.UserProfile).WithMany(w => w.TopicMessages).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.Topic).WithMany(w => w.Messages).HasForeignKey(q => q.TopicId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.ParentMessage).WithMany(q => q.Replies).HasForeignKey(q => q.ParentMessageId);

        }
    }
}
