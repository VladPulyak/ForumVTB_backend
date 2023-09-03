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
    public class FindFavouriteConfigurations : IEntityTypeConfiguration<FindFavourite>
    {
        public void Configure(EntityTypeBuilder<FindFavourite> builder)
        {
            builder.ToTable("FindFavourites");
            builder.HasKey(q => q.Id);
            builder.HasOne(q => q.User).WithMany(w => w.FindFavourites).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.Find).WithMany(w => w.Favourites).HasForeignKey(q => q.FindId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(q => new
            {
                q.UserId,
                q.FindId
            }).IsUnique();
        }
    }
}
