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
    public class FavouritesConfigurations : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
            builder.ToTable("Favourites");
            builder.HasKey(q => q.Id);
            builder.HasOne(q => q.User).WithMany(w => w.Favourites).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.Advert).WithMany(w => w.Favourites).HasForeignKey(q => q.AdvertId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(q => new
            {
                q.UserId,
                q.AdvertId
            }).IsUnique();
        }
    }
}
