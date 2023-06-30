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
    public class SubsectionConfigurations : IEntityTypeConfiguration<Subsection>
    {
        public void Configure(EntityTypeBuilder<Subsection> builder)
        {
            builder.ToTable("Subsections");
            builder.HasKey(x => x.Id);
            builder.Property(q=>q.Name).IsRequired();
            builder.Property(q => q.Name).HasMaxLength(40);
            builder.HasOne(q=>q.Section).WithMany(w=>w.Subsections).HasForeignKey(q=>q.SectionId);
        }
    }
}
