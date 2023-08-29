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
    public class UserChatConfigurations : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.ToTable("UserChats");
            builder.HasKey(q => q.Id);
            builder.HasOne(q => q.FirstUser).WithMany(w => w.ChatsAsFirstUser).HasForeignKey(q => q.FirstUserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(q => q.SecondUser).WithMany(w => w.ChatsAsSecondUser).HasForeignKey(q => q.SecondUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
