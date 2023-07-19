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
    public class AdvertFileConfugiration : IEntityTypeConfiguration<AdvertFile>
    {
        public void Configure(EntityTypeBuilder<AdvertFile> builder)
        {
            builder.ToTable("AdvertFiles");
            builder.HasKey(x => x.Id);
            builder.Property(q => q.FileURL).IsRequired();
            builder.HasOne(q => q.Advert).WithMany(w => w.Files).HasForeignKey(q => q.AdvertId);
        }
    }
}
