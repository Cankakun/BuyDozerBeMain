using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<HeavyUnit> HeavyUnits { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<DetailRent> DetailRents { get; }
    DbSet<DetailBuy> DetailBuys { get; }
    DbSet<PriceListRent> PriceListRents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
