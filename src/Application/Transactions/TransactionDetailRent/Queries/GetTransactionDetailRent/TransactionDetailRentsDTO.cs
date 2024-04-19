using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailRent.Queries.GetTransactionDetailRent;

public class TransactionDetailRentsDTO
{
    public DateOnly DateRent { get; set; }
    public DateOnly DateReturn { get; set; }
    public Transaction Transaction { get; set; } = null!;
    private class Mapping : Profile
    {
        public Mapping()
        {
            // CreateMap<Transaction, TransactionDetailRentsDTO>();
            CreateMap<DetailRent, TransactionDetailRentsDTO>();
        }
    }
}
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
    public DetailRent DetailRents { get; set; } = null!;

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