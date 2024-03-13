using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.HeavyUnits.Commands.UpdateHeavyUnit;

public record UpdateHeavyUnitCommand : IRequest
{
    public required string Id { get; init; }
    public string? NameUnit { get; init; }
    public string? TypeUnit { get; init; }
    public string? DescUnit { get; init; }
    public string? ImgUnit { get; init; }
    public string? ImgBrand { get; init; }
    public decimal PriceBuyUnit { get; init; }
    public decimal PriceRentUnit { get; init; }
    public int QtyUnit { get; init; }
}

public class UpdateHeavyUnitCommandHandler : IRequestHandler<UpdateHeavyUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateHeavyUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateHeavyUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.HeavyUnits
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.NameUnit = request.NameUnit;
        entity.TypeUnit = request.TypeUnit;
        entity.DescUnit = request.DescUnit;
        entity.ImgUnit = request.ImgUnit;
        entity.ImgBrand = request.ImgBrand;
        entity.PriceBuyUnit = request.PriceBuyUnit;
        entity.PriceRentUnit = request.PriceRentUnit;
        entity.QtyUnit = request.QtyUnit;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
