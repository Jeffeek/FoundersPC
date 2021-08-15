using FoundersPC.API.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
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
        }
    }
}