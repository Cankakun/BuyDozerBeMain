using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailBuy.Queries.GetTransactionDetailBuy;
public class TransactionDTO
{
    public string? Id { get; init; }
    public string? TransactionNum { get; init; }
    public string? NameUnit { get; init; } = null!;
    public string? TypeUnit { get; init; } = null!;
    public string? UserName { get; init; } = null!;
    public decimal PriceBuyUnit { get; init; }
    public string? ReceiverName { get; init; }
    public string? ReceiverHp { get; init; }
    public string? ReceiverAddress { get; init; }
    public int QtyTransaction { get; init; }
    public decimal TotalPriceTransaction { get; init; }
    public DateOnly DateTransaction { get; init; }
    public int StatusTransaction { get; init; }
    public DateOnly DateBuy { get; set; }
    public DateTimeOffset Created { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Transaction, TransactionDTO>()
            .ForMember(d => d.NameUnit, opt => opt.MapFrom(src => src.Unit.NameUnit))
            .ForMember(d => d.TypeUnit, opt => opt.MapFrom(src => src.Unit.TypeUnit))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(d => d.PriceBuyUnit, opt => opt.MapFrom(src => src.Unit.PriceBuyUnit))
            .ForMember(d => d.DateBuy, opt => opt.MapFrom(src => src.DetailBuy.DateBuy));
        }
    }
}