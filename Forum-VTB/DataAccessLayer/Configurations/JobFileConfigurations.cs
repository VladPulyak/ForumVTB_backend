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
    public class JobFileConfigurations : IEntityTypeConfiguration<JobFile>
    {
        public void Configure(EntityTypeBuilder<JobFile> builder)
        {
            builder.ToTable("JobFiles");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.FileURL).IsRequired();
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp");
            builder.HasOne(q => q.Job).WithMany(w => w.Files).HasForeignKey(q => q.JobId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
