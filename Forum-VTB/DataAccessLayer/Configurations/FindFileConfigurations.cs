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
    public class FindFileConfigurations : IEntityTypeConfiguration<FindFile>
    {
        public void Configure(EntityTypeBuilder<FindFile> builder)
        {
            builder.ToTable("FindFiles");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.FileURL).IsRequired();
            builder.Property(q => q.DateOfCreation).HasColumnType("timestamp");
            builder.HasOne(q => q.Find).WithMany(w => w.Files).HasForeignKey(q => q.FindId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
