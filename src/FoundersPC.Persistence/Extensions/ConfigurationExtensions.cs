using FoundersPC.API.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddAuditableColumns<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : FullAuditable
        {
            builder.Property(p => p.Created)
                   .HasColumnName("Created")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(p => p.CreatedById)
                   .HasColumnName("CreatedById")
                   .HasColumnType("int")
                   .IsRequired();

            builder.HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedById)
                   .IsRequired();

            builder.Property(p => p.LastModified)
                   .HasColumnName("LastModified")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(p => p.LastModifiedById)
                   .HasColumnName("LastModifiedById")
                   .HasColumnType("int")
                   .IsRequired();

            builder.HasOne(x => x.LastModifiedBy)
                   .WithMany()
                   .HasForeignKey(x => x.LastModifiedById)
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                   .HasColumnName("IsDeleted")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(p => p.Deleted)
                   .HasColumnName("Deleted")
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.Property(p => p.DeletedById)
                   .HasColumnName("DeletedById")
                   .HasColumnType("int")
                   .IsRequired(false);

            builder.HasOne(x => x.DeletedBy)
                   .WithMany()
                   .HasForeignKey(x => x.DeletedById)
                   .IsRequired(false);

            builder.Navigation(p => p.CreatedBy)
                   .AutoInclude();

            builder.Navigation(p => p.LastModifiedBy)
                   .AutoInclude();

            builder.Navigation(p => p.DeletedBy)
                   .AutoInclude();
        }
    }
}
