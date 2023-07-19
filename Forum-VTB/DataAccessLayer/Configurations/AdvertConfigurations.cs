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
    public class AdvertConfigurations : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> builder)
        {
            builder.ToTable("Adverts");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).IsRequired();
            builder.Property(q => q.Title).HasMaxLength(30);
            builder.Property(q => q.Description).IsRequired();
            builder.Property(q => q.Description).HasMaxLength(3000);
            builder.Property(q => q.Price).IsRequired();
            builder.Property(q => q.Price).HasMaxLength(10);
            builder.HasOne(q => q.Subsection).WithMany(w => w.Adverts).HasForeignKey(q => q.SubsectionId);
            builder.HasOne(q => q.User).WithMany(w => w.Adverts).HasForeignKey(q => q.UserId);
        }
    }
}
