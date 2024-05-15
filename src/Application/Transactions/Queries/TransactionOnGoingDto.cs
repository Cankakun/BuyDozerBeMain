using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.TransactionOnGoing.Queries.GetTransactionOnGoing;

public class TransactionOnGoingDTO
{
    public string? Id { get; init; }
    public string? TransactionNum { get; init; }
    public string? UserName { get; init; }
    public string? ReceiverName { get; init; }
    public string? ReceiverHp { get; init; }
    public string? ReceiverAddress { get; init; }
    public int QtyTransaction { get; init; }
    public decimal TotalPriceTransaction { get; init; }
    public string? PaymentConfirmationReceipt { get; set; }
    public DateOnly DateTransaction { get; init; }
    public int StatusTransaction { get; init; }
    public bool IsBuy { get; init; }
    public DateTimeOffset Created { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Transaction, TransactionOnGoingDTO>()
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(d => d.IsBuy, opt => opt.MapFrom(src => src.DetailBuy != null && src.DetailRents == null ? true : src.DetailRents != null ? false : (bool?)null));
        }
    }
}