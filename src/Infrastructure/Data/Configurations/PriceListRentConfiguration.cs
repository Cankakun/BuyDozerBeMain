using BuyDozerBeMain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuyDozerBeMain.Infrastructure.Data.Configurations;

public class PriceListRentConfiguration : IEntityTypeConfiguration<PriceListRent>
{
    public void Configure(EntityTypeBuilder<PriceListRent> builder)
    {
        builder.Property(t => t.PriceRentUnit)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}
