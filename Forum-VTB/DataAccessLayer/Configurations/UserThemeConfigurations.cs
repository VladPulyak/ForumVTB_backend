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
    public class UserThemeConfigurations : IEntityTypeConfiguration<UserTheme>
    {
        public void Configure(EntityTypeBuilder<UserTheme> builder)
        {
            builder.ToTable("UserThemes");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Theme).IsRequired().HasMaxLength(10);
            builder.HasOne(q => q.User).WithOne(q => q.Theme).HasForeignKey<UserTheme>(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
