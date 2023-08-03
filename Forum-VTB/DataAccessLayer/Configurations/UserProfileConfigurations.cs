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
            builder.HasKey(q => q.Id);
            builder.ToTable("UserProfiles");
            builder.Property(q => q.UserName).HasMaxLength(30);
            builder.Property(q => q.NickName).HasMaxLength(30);
            builder.Property(q => q.BirthDate).HasColumnType("timestamp");
            builder.Property(q => q.Email).HasMaxLength(30);
            builder.HasIndex(q => new
            {
                q.UserName
            }).IsUnique();
        }
    }
}