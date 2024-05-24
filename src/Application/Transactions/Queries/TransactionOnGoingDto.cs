using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.TransactionOnGoing.Queries.GetTransactionOnGoing;

public class TransactionOnGoingDTO
{
    public string? Id { get; init; }
    public string? TransactionNum { get; init; }
    public string? NameUnit { get; init; }
    public string? TypeUnit { get; init; }
    public string? ImgUnit { get; init; }
    public string? UserName { get; init; }
    public string? ReceiverName { get; init; }
    public string? ReceiverHp { get; init; }
    public string? ReceiverAddress { get; init; }
    public int QtyTransaction { get; init; }
    public int Months { get; init; }
    public decimal TotalPriceTransaction { get; init; }
    public string? PaymentConfirmationReceipt { get; set; }
    public DateOnly DateTransaction { get; init; }
    public int StatusTransaction { get; init; }
    public bool IsBuy { get; init; }
    public DateTimeOffset Created { get; set; }
    public DateOnly? DateRent { get; set; }
    public DateOnly? DateReturn { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Transaction, TransactionOnGoingDTO>()
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(d => d.NameUnit, opt => opt.MapFrom(src => src.Unit.NameUnit))
            .ForMember(d => d.TypeUnit, opt => opt.MapFrom(src => src.Unit.TypeUnit))
            .ForMember(d => d.ImgUnit, opt => opt.MapFrom(src => src.Unit.ImgUnit))
            .ForMember(d => d.Months, opt => opt.MapFrom(src => src.DetailRents != null ? src.PriceListRent.Months : 0))
            .ForMember(d => d.IsBuy, opt => opt.MapFrom(src => src.DetailBuy != null && src.DetailRents == null ? true : src.DetailRents != null ? false : (bool?)null))
            .ForMember(d => d.DateRent, opt => opt.MapFrom(src => src.DetailRents != null ? src.DetailRents.DateRent : (DateOnly?)null))
            .ForMember(d => d.DateReturn, opt => opt.MapFrom(src => src.DetailRents != null ? src.DetailRents.DateReturn : (DateOnly?)null));
            ;
        }
    }
}