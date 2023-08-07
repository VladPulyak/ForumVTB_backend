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
            builder.Property(q => q.Description).IsRequired().HasMaxLength(300);
            builder.Property(q => q.OtherInfo).IsRequired().HasMaxLength(500);
            builder.HasOne(q => q.Subsection).WithMany(q => q.Events).HasForeignKey(q => q.SubsectionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
