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
    public class MessageConfigurations : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("TopicMessages");
            builder.HasKey(x => x.Id);
            builder.Property(q=>q.Text).IsRequired();
            builder.Property(q=>q.Text).HasMaxLength(300);
            builder.HasOne(q => q.UserProfile).WithMany(w => w.Messages).HasForeignKey(q => q.UserId);
            builder.HasOne(q => q.Topic).WithMany(w => w.Messages).HasForeignKey(q => q.TopicId);
            builder.HasOne(q => q.ReplyingMessage).WithOne(q => q.ReplyingMessage).HasForeignKey<Message>(q => q.ReplyingMessageId);
        }
    }
}
