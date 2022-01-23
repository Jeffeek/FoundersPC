using FoundersPC.Domain.Entities.Identity.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Identity;

public class AccessTokenHistoryConfiguration : IEntityTypeConfiguration<AccessTokenHistory>
{
    public void Configure(EntityTypeBuilder<AccessTokenHistory> builder)
    {
        builder.ToTable("AccessTokensHistory");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AccessTokenId)
               .HasColumnName("AccessTokenId")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(x => x.RequestDate)
               .HasColumnName("RequestDate")
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(x => x.RequestUserId)
               .HasColumnName("RequestUserId")
               .HasColumnType("int")
               .IsRequired();

        builder.HasOne(x => x.RequestUser)
               .WithMany(x => x.TokensHistories)
               .HasForeignKey(x => x.RequestUserId)
               .IsRequired();

        builder.HasOne(x => x.AccessToken)
               .WithMany(x => x.History)
               .HasForeignKey(x => x.AccessTokenId)
               .IsRequired();
    }
}