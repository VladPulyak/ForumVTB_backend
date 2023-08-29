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
    public class TopicFileConfigurations : IEntityTypeConfiguration<TopicFile>
    {
        public void Configure(EntityTypeBuilder<TopicFile> builder)
        {
            builder.ToTable("TopicFiles");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.FileURL).IsRequired();
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp");
            builder.HasOne(q => q.Topic).WithMany(w => w.Files).HasForeignKey(q => q.TopicId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
