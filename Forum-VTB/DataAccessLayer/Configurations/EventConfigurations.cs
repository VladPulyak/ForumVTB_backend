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
    public class EventConfigurations : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Description).IsRequired().HasMaxLength(3000);
            builder.Property(q => q.Address).IsRequired().HasMaxLength(300);
            builder.Property(q => q.Price).IsRequired().HasMaxLength(10);
            builder.Property(q => q.PhoneNumber).IsRequired().HasMaxLength(13);
            builder.Property(q => q.StartDate).HasColumnType("timestamp with time zone");
            builder.Property(q => q.EndDate).HasColumnType("timestamp with time zone");
            builder.HasOne(q => q.Subsection).WithMany(q => q.Events).HasForeignKey(q => q.SubsectionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
