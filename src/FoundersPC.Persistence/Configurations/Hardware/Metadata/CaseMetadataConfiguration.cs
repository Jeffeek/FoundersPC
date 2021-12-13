using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class CaseMetadataConfiguration : IEntityTypeConfiguration<CaseMetadata>
{
    public void Configure(EntityTypeBuilder<CaseMetadata> builder)
    {
        builder.Property(x => x.WindowMaterial)
               .HasColumnName("WindowMaterial")
               .HasColumnType("nvarchar")
               .IsRequired(false);

        builder.Property(x => x.CaseTypeId)
               .HasColumnName("CaseTypeId")
               .HasColumnType("int")
               .IsRequired(false);

        builder.Property(x => x.Color)
               .HasColumnType("nvarchar")
               .HasColumnType("Color")
               .IsRequired(false);

        builder.Property(x => x.MaxMotherboardSize)
               .HasColumnType("numeric")
               .HasColumnName("MaxMotherboardSize")
               .IsRequired(false);

        builder.Property(x => x.Material)
               .HasColumnType("nvarchar")
               .HasColumnName("Material")
               .IsRequired(false);

        builder.Property(x => x.TransparentWindow)
               .HasColumnType("bit")
               .HasColumnName("TransparentWindow")
               .IsRequired(false);

        builder.Property(x => x.Weight)
               .HasColumnType("numeric")
               .HasColumnName("Weight")
               .IsRequired(false);

        builder.Property(x => x.Height)
               .HasColumnType("numeric")
               .HasColumnName("Height")
               .IsRequired(false);

        builder.Property(x => x.Width)
               .HasColumnType("numeric")
               .HasColumnName("Width")
               .IsRequired(false);

        builder.Property(x => x.Depth)
               .HasColumnType("numeric")
               .HasColumnName("Depth")
               .IsRequired(false);

        builder.HasOne(x => x.CaseType)
               .WithMany()
               .HasForeignKey(x => x.CaseTypeId)
               .IsRequired(false);

        builder.HasOne(x => x.Case)
               .WithOne(x => x.Metadata)
               .HasForeignKey<CaseMetadata>(x => x.Id)
               .IsRequired();
    }
}