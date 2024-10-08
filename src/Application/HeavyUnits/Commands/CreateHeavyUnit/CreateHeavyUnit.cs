using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;
[Authorize(Roles = "Administrator")]
public record CreateHeavyUnitCommand : IRequest<string>
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

public class CreateHeavyUnitCommandHandler : IRequestHandler<CreateHeavyUnitCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreateHeavyUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateHeavyUnitCommand request, CancellationToken cancellationToken)
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
