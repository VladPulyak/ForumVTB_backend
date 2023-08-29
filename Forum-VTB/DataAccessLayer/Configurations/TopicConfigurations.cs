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
    public class TopicConfigurations : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topics");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).IsRequired();
            builder.Property(q => q.Title).HasMaxLength(100);
            builder.Property(q => q.Description).IsRequired();
            builder.Property(q => q.Description).HasMaxLength(3000);
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp with time zone");
            builder.HasOne(q => q.Subsection).WithMany(w => w.Topics).HasForeignKey(q => q.SubsectionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.User).WithMany(w => w.Topics).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
