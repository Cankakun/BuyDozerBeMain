using BuyDozerBeMain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuyDozerBeMain.Infrastructure.Data.Configurations;

public class HeavyUnitConfiguration : IEntityTypeConfiguration<HeavyUnit>
{
    public void Configure(EntityTypeBuilder<HeavyUnit> builder)
    {
        builder.Property(t => t.NameUnit)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.TypeUnit)
            .HasMaxLength(25)
            .IsRequired();
        builder.Property(t => t.PriceBuyUnit)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Property(t => t.PriceRentUnit)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Property(t => t.QtyUnit)
            .HasMaxLength(10)
            .IsRequired();
        // builder.Property(t => t.Id)
        // .HasConversion(v => v.ToString(), v => Guid.Parse(v));
    }
}
