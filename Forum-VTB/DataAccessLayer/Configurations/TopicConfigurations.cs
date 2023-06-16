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
            builder.Property(q => q.Name).IsRequired();
            builder.Property(q => q.Name).HasMaxLength(30);
            builder.HasOne(q => q.Subsection).WithMany(w => w.Topics).HasForeignKey(q => q.SubsectionId);
        }
    }
}
