using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.PriceListRents.Commands.UpdatePriceListRent;
[Authorize(Roles = "Administrator")]

public record UpdatePriceListRentCommand : IRequest
{
    public required string Id { get; set; }
    public string? NameRent { get; init; }
    public decimal PriceRentUnit { get; init; }
    public int Months { get; init; }
}

public class UpdatePriceListRentCommandHandler : IRequestHandler<UpdatePriceListRentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePriceListRentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePriceListRentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PriceListRents
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.NameRent = request.NameRent;
        entity.PriceRentUnit = request.PriceRentUnit;
        entity.Months = request.Months;


        await _context.SaveChangesAsync(cancellationToken);
    }
}
