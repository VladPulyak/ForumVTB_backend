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
    public class WorkFileConfigurations : IEntityTypeConfiguration<WorkFile>
    {
        public void Configure(EntityTypeBuilder<WorkFile> builder)
        {
            builder.ToTable("WorkFiles");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.FileURL).IsRequired();
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp");
            builder.HasOne(q => q.Work).WithMany(w => w.Files).HasForeignKey(q => q.WorkId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
