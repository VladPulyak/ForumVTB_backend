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
    public class UserProfileConfigurations : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.NickName).IsRequired();
            builder.Property(q => q.NickName).HasMaxLength(30);
            builder.Property(q => q.Email).IsRequired();
            builder.Property(q => q.Email).HasMaxLength(50);
            builder.Property(q => q.Phone).IsRequired();
            builder.Property(q => q.Phone).HasMaxLength(15);
        }
    }
}
