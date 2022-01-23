using FoundersPC.Domain.Entities.Identity.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Identity;

public class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
{
    public void Configure(EntityTypeBuilder<AccessToken> builder)
    {
        builder.ToTable("AccessTokens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExpirationDate)
               .HasColumnName("ExpirationDate")
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(x => x.Token)
               .HasColumnName("Token")
               .HasColumnType("nvarchar")
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(x => x.UserId)
               .HasColumnName("UserId")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(x => x.Type)
               .HasColumnName("Type")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(x => x.StartEvaluationDate)
               .HasColumnName("StartEvaluationDate")
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(x => x.IsBlocked)
               .HasColumnName("IsBlocked")
               .HasColumnType("bit")
               .IsRequired();

        builder.HasOne(x => x.ApplicationUser)
               .WithMany(x => x.Tokens)
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.HasMany(x => x.History)
               .WithOne(x => x.AccessToken)
               .HasForeignKey(x => x.AccessTokenId)
               .IsRequired();
    }
}