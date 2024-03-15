using BuyDozerBeMain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuyDozerBeMain.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(t => t.CompanyUser)
            .HasMaxLength(25)
            .IsRequired();
        builder.Property(t => t.PositionUser)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.UserName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.NormalizedUserName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.Email)
            .HasMaxLength(60)
            .IsRequired();
        builder.Property(t => t.NormalizedEmail)
            .HasMaxLength(60)
            .IsRequired();
        builder.Property(t => t.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

    }
}
