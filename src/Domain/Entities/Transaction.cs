namespace BuyDozerBeMain.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    public string? TransactionNum { get; set; }
    public string? UnitId { get; set; }
    public string? UserId { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverHp { get; set; }
    public string? ReceiverAddress { get; set; }
    public int QtyTransaction { get; set; }
    public decimal TotalPriceTransaction { get; set; }
    public DateOnly DateTransaction { get; set; }
    public int StatusTransaction { get; set; } = 1;
    public HeavyUnit Unit { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public DetailRent DetailRents { get; set; } = null!;
    public DetailBuy DetailBuy { get; set; } = null!;

}
