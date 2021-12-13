﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class SolidStateDriveMetadataConfiguration : IEntityTypeConfiguration<SolidStateDriveMetadata>
{
    public void Configure(EntityTypeBuilder<SolidStateDriveMetadata> builder)
    {
        builder.Property(x => x.DiskFactorId)
               .HasColumnName("DiskFactorId")
               .HasColumnType("int")
               .IsRequired(false);

        builder.Property(x => x.DiskConnectionInterfaceId)
               .HasColumnType("int")
               .HasColumnName("DiskConnectionInterfaceId")
               .IsRequired(false);

        builder.Property(x => x.Volume)
               .HasColumnType("int")
               .HasColumnName("Volume")
               .IsRequired(false);

        builder.Property(x => x.MicroScheme)
               .HasColumnType("nvarchar")
               .HasColumnName("MicroScheme")
               .HasMaxLength(2048)
               .IsRequired(false);

        builder.Property(x => x.SequentialRead)
               .HasColumnType("numeric")
               .HasColumnName("SequentialRead")
               .IsRequired(false);

        builder.Property(x => x.SequentialRecording)
               .HasColumnType("numeric")
               .HasColumnName("SequentialRecording")
               .IsRequired(false);

        builder.HasOne(x => x.Factor)
               .WithMany()
               .HasForeignKey(x => x.DiskFactorId)
               .IsRequired(false);

        builder.HasOne(x => x.Interface)
               .WithMany()
               .HasForeignKey(x => x.DiskConnectionInterfaceId)
               .IsRequired(false);

        builder.HasOne(x => x.SolidStateDrive)
               .WithOne(x => x.Metadata)
               .HasForeignKey<SolidStateDriveMetadata>(x => x.Id)
               .IsRequired();
    }
}