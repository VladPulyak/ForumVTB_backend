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
    public class WorkFavouriteConfigurations : IEntityTypeConfiguration<WorkFavourite>
    {
        public void Configure(EntityTypeBuilder<WorkFavourite> builder)
        {
            builder.ToTable("WorkFavourites");
            builder.HasKey(q => q.Id);
            builder.HasOne(q => q.User).WithMany(w => w.WorkFavourites).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.Work).WithMany(w => w.Favourites).HasForeignKey(q => q.WorkId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(q => new
            {
                q.UserId,
                q.WorkId
            }).IsUnique();

        }
    }
}
