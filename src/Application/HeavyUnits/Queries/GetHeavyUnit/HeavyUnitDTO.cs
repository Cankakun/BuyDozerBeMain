using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.HeavyUnits.Queries.GetHeavyUnit;
public class HeavyUnitDTO
{
    public string? Id { get; init; }
    public string? NameUnit { get; init; }
    public string? TypeUnit { get; init; }
    public string? DescUnit { get; init; }
    public string? ImgUnit { get; init; }
    public string? ImgBrand { get; init; }
    public decimal PriceBuyUnit { get; init; }
    public decimal PriceRentUnit { get; init; }
    public int QtyUnit { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<HeavyUnit, HeavyUnitDTO>();
        }
    }
}