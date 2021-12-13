using FoundersPC.Domain.Entities.Metadata;
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
    }
}