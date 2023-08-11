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
    public class UserMessageConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.ToTable("UserMessages");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.Text).IsRequired();
            builder.Property(q => q.Text).HasMaxLength(300);
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp with time zone").IsRequired();
            builder.HasOne(q => q.Sender).WithMany(w => w.SentMessages).HasForeignKey(q => q.SenderId);
            builder.HasOne(q => q.Receiver).WithMany(w => w.ReceivedMessages).HasForeignKey(q => q.ReceiverId);
            builder.HasOne(q => q.ParentMessage).WithMany().HasForeignKey(q => q.ParentMessageId);
            builder.HasOne(q => q.Chat).WithMany(w=>w.Messages).HasForeignKey(q => q.ChatId);
        }
    }
}
