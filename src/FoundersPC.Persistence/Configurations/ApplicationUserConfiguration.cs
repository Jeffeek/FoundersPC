using System;
using FoundersPC.API.Domain.Entities.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Login)
                   .HasColumnName("Login")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(x => x.RegistrationDate)
                   .HasColumnName("RegistrationDate")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(x => x.RoleId)
                   .HasColumnName("RoleId")
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .HasColumnName("IsActive")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(x => x.IsBlocked)
                   .HasColumnName("IsBlocked")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(x => x.SendMessageOnEntrance)
                   .HasColumnName("SendMessageOnEntrance")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(x => x.SendMessageOnApiRequest)
                   .HasColumnName("SendMessageOnApiRequest")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.HasOne(x => x.ApplicationRole)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.RoleId)
                   .IsRequired();

            builder.HasMany(x => x.Tokens)
                   .WithOne(x => x.ApplicationUser)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();

            builder.HasMany(x => x.Entrances)
                   .WithOne(x => x.ApplicationUser)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasColumnName("Email")
                   .HasColumnType("nvarchar")
                   .IsRequired();

            builder.Property(x => x.EmailConfirmed)
                   .HasColumnName("EmailConfirmed")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(x => x.PasswordHash)
                   .HasColumnName("PasswordHash")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(60)
                   .IsRequired();

            builder.Ignore(x => x.AccessFailedCount);
            builder.Ignore(x => x.ConcurrencyStamp);
            builder.Ignore(x => x.UserName);
            builder.Ignore(x => x.NormalizedUserName);
            builder.Ignore(x => x.TwoFactorEnabled);
            builder.Ignore(x => x.SecurityStamp);
            builder.Ignore(x => x.PhoneNumberConfirmed);
            builder.Ignore(x => x.PhoneNumber);
            builder.Ignore(x => x.NormalizedEmail);
            builder.Ignore(x => x.LockoutEnd);
            builder.Ignore(x => x.LockoutEnabled);
        }
    }
}