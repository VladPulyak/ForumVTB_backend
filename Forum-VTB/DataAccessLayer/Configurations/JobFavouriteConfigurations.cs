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
    public class JobFavouriteConfigurations : IEntityTypeConfiguration<JobFavourite>
    {
        public void Configure(EntityTypeBuilder<JobFavourite> builder)
        {
            builder.ToTable("JobFavourites");
            builder.HasKey(q => q.Id);
            builder.HasOne(q => q.User).WithMany(w => w.JobFavourites).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.Job).WithMany(w => w.Favourites).HasForeignKey(q => q.JobId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(q => new
            {
                q.UserId,
                q.JobId
            }).IsUnique();

        }
    }
}
