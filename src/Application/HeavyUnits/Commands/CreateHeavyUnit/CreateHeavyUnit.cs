using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;

public record CreateHeavyUnitCommand : IRequest<int>
{
    public string? NameUnit { get; init; }
    public string? TypeUnit { get; init; }
    public string? DescUnit { get; init; }
    public string? ImgUnit { get; init; }
    public string? ImgBrand { get; init; }
    public decimal PriceBuyUnit { get; init; }
    public decimal PriceRentUnit { get; init; }
    public int QtyUnit { get; init; }
}

public class CreateHeavyUnitCommandHandler : IRequestHandler<CreateHeavyUnitCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateHeavyUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateHeavyUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = new HeavyUnit
        {
            NameUnit = request.NameUnit,
            TypeUnit = request.TypeUnit,
            DescUnit = request.DescUnit,
            ImgBrand = request.ImgBrand,
            ImgUnit = request.ImgUnit,
            PriceBuyUnit = request.PriceBuyUnit,
            PriceRentUnit = request.PriceRentUnit,
            QtyUnit = request.QtyUnit,
        };

        _context.HeavyUnits.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
