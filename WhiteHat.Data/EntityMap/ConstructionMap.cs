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
    public class ConstructionMap : IEntityTypeConfiguration<Construction>
    {
        public void Configure(EntityTypeBuilder<Construction> builder)
        {
            builder.HasKey(x => x.ConstructionId);
            builder.Property(x => x.Title).IsRequired(true).HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(x => x.Image).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(256).HasColumnType("nvarchar");
            builder.Property(x => x.ReadMore).IsRequired(false).HasColumnType("nvarchar(max)");
            builder.Property(x => x.IsDeleted).HasAnnotation("DefaultValue", "false");

        }
    }
}
