using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<HeavyUnit> HeavyUnits { get; }
    DbSet<Transaction> Transactions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
