using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Models;

namespace WhiteHat.Data.EntityMap
{
    public class UserInfoMap : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.GenderId).IsRequired(false);
            builder.Property(x => x.DOB).IsRequired(false);
            builder.Property(x => x.IsDeleted).HasAnnotation("DefaultValue", "false");
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.MiddleName).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Mobile).IsRequired(false).HasMaxLength(50).HasColumnType("nvarchar");
        }

        private static void NewMethod(EntityTypeBuilder<UserInfo> builder)
        {
            builder.Property(x => x.Image).IsRequired(false).HasColumnType("nvarchar(max)");
        }
    }
}
