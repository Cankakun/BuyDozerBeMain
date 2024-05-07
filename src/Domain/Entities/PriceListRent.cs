namespace BuyDozerBeMain.Domain.Entities;

public class PriceListRent : BaseEntity
{
    public string? NameRent { get; set; }
    public decimal PriceRentUnit { get; set; }
    public int Months { get; set; }
}
