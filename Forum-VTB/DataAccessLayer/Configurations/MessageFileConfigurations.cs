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
    public class MessageFileConfigurations : IEntityTypeConfiguration<MessageFile>
    {
        public void Configure(EntityTypeBuilder<MessageFile> builder)
        {
            builder.ToTable("MessageFiles");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.FileURL).IsRequired();
            builder.Property(q => q.FileURL).HasMaxLength(150);
            builder.HasOne(q => q.Message).WithMany(w => w.Files).HasForeignKey(q => q.MessageId);
        }
    }
}
