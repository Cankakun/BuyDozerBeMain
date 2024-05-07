using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.PriceListRents.Queries.GetPriceListRent;
public class PriceListRentDTO
{
    public string? Id { get; init; }
    public string? NameRent { get; set; }
    public decimal PriceRentUnit { get; set; }
    public int Months { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PriceListRent, PriceListRentDTO>();
        }
    }
}