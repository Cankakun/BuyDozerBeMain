using BuyDozerBeMain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuyDozerBeMain.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(t => t.TotalPriceTransaction)
            .HasColumnType("decimal(18,2)");
        builder.Property(t => t.TransactionNum)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(t => t.ReceiverName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.ReceiverAddress)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.ReceiverHp)
            .HasMaxLength(13)
            .IsRequired();
        builder.Property(t => t.QtyTransaction)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(t => t.QtyTransaction)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(t => t.StatusTransaction)
            .HasMaxLength(1)
            .IsRequired();
    }
}
