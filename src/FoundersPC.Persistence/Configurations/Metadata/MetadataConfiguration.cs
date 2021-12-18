using FoundersPC.Domain.Entities.Metadata;
using FoundersPC.Domain.Enums;
using FoundersPC.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Metadata;

public class MetadataConfiguration : IEntityTypeConfiguration<MetadataEntity>
{
    public void Configure(EntityTypeBuilder<MetadataEntity> builder)
    {
        builder.ToTable("Metadata");
        builder.HasKey(x => x.Id);
        builder.AddAuditableColumns();

        builder.Property(x => x.Name)
               .HasColumnName("Name")
               .HasColumnType("nvarchar")
               .IsRequired();

        builder.HasDiscriminator<int>("Type")
               .HasValue<CaseType>((int)MetadataType.CaseType)
               .HasValue<DiskConnectionInterface>((int)MetadataType.DiskConnectionInterface)
               .HasValue<DiskFactor>((int)MetadataType.DiskFactor)
               .HasValue<MotherboardFactor>((int)MetadataType.MotherboardFactor)
               .HasValue<RamMode>((int)MetadataType.RamMode)
               .HasValue<RamType>((int)MetadataType.RAMType)
               .HasValue<Socket>((int)MetadataType.Socket)
               .HasValue<WindowMaterial>((int)MetadataType.WindowMaterial)
               .HasValue<Color>((int)MetadataType.Color)
               .HasValue<Material>((int)MetadataType.Material)
               .HasValue<TechProcess>((int)MetadataType.TechProcess)
               .HasValue<Country>((int)MetadataType.Country)
               .HasValue<MotherboardPowering>((int)MetadataType.MotherboardPowering)
               .HasValue<VideoMemory>((int)MetadataType.VideoMemory);
    }
}