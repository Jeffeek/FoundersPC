#region Using namespaces

using FoundersPC.Domain.Entities.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Identity;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(x => x.Name)
               .HasColumnName("Name")
               .HasColumnType("nvarchar")
               .IsRequired();

        builder.HasMany(x => x.Users)
               .WithOne(x => x.ApplicationRole)
               .HasForeignKey(x => x.RoleId)
               .IsRequired();

        builder.Ignore(x => x.ConcurrencyStamp);
        builder.Ignore(x => x.NormalizedName);
    }
}