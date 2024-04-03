namespace BuyDozerBeMain.Domain.Entities;

public class DetailRent : BaseEntity
{
    public string? TransactionId { get; set; }
    public DateOnly DateRent { get; set; }
    public DateOnly DateReturn { get; set; }
    public Transaction Transaction { get; private set; } = null!;
}
