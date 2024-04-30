using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailBuy.Queries.GetTransactionDetailBuy;
public class TransactionDTO
{
    public string? Id { get; init; }
    public string? TransactionNum { get; init; }
    public HeavyUnit Unit { get; init; } = null!;
    public UserEntity User { get; init; } = null!;
    public string? ReceiverName { get; init; }
    public string? ReceiverHp { get; init; }
    public string? ReceiverAddress { get; init; }
    public int QtyTransaction { get; init; }
    public decimal TotalPriceTransaction { get; init; }
    public DateOnly DateTransaction { get; init; }
    public int StatusTransaction { get; init; }
    public DetailBuy DetailBuys { get; set; } = null!;

    // public DetailRent DetailRents { get; init; } = null!;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Transaction, TransactionDTO>();
            // CreateMap<DetailRent, TransactionDTO>();
        }
    }
}