﻿using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class CaseMetadataConfiguration : IEntityTypeConfiguration<CaseMetadata>
{
    public void Configure(EntityTypeBuilder<CaseMetadata> builder)
    {
        builder.Property(x => x.WindowMaterialId)
               .HasColumnName("WindowMaterialId")
               .HasColumnType("int")
               .IsRequired(false);

        builder.Property(x => x.CaseTypeId)
               .HasColumnName("CaseTypeId")
               .HasColumnType("int")
               .IsRequired(false);

        builder.Property(x => x.ColorId)
               .HasColumnType("int")
               .HasColumnName("ColorId")
               .IsRequired(false);

        builder.Property(x => x.MaxMotherboardSize)
               .HasColumnType("numeric")
               .HasColumnName("MaxMotherboardSize")
               .IsRequired(false);

        builder.Property(x => x.MaterialId)
               .HasColumnType("int")
               .HasColumnName("MaterialId")
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