namespace BuyDozerBeMain.Domain.Entities;

public class DetailBuy : BaseEntity
{
    public Transaction Transaction { get; set; } = null!;
    public DateOnly DateBuy { get; set; }
}
