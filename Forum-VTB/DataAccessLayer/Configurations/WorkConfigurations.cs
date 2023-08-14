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
    public class WorkConfigurations : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Works");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).IsRequired();
            builder.Property(q => q.Title).HasMaxLength(100);
            builder.Property(q => q.Description).IsRequired();
            builder.Property(q => q.Description).HasMaxLength(3000);
            builder.Property(q => q.Price).IsRequired();
            builder.Property(q => q.Price).HasMaxLength(10);
            builder.Property(q => q.PhoneNumber).HasMaxLength(13);
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp with time zone");
            builder.HasOne(q => q.Subsection).WithMany(w => w.Works).HasForeignKey(q => q.SubsectionId);
            builder.HasOne(q => q.User).WithMany(w => w.Works).HasForeignKey(q => q.UserId);

        }
    }
}
