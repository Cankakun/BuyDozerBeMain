namespace BuyDozerBeMain.Domain.Entities;

public class HeavyUnit : BaseAuditableEntity
{
    public string? NameUnit { get; set; }
    public string? TypeUnit { get; set; }
    public string? DescUnit { get; set; }
    public string? ImgUnit { get; set; }
    public string? ImgBrand { get; set; }
    public decimal PriceBuyUnit { get; set; }
    public decimal PriceRentUnit { get; set; }
    public int QtyUnit { get; set; }
}
