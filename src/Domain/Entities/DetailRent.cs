namespace BuyDozerBeMain.Domain.Entities;

public class DetailRent : BaseEntity
{
    public Transaction Transaction { get; set; } = null!;
    public DateOnly DateRent { get; set; }
    public DateOnly DateReturn { get; set; }
}
