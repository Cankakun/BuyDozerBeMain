namespace BuyDozerBeMain.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    public string? TransactionNum { get; set; }
    public HeavyUnit Unit { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public string? ReceiverName { get; set; }
    public string? ReceiverHp { get; set; }
    public string? ReceiverAddress { get; set; }
    public int QtyTransaction { get; set; }
    public decimal TotalPriceTransaction { get; set; }
    public DateOnly DateTransaction { get; set; }
    public int StatusTransaction { get; set; } = 1;

}
