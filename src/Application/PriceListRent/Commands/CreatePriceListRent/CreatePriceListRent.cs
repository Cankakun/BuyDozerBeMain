using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.PriceListRents.Commands.CreatePriceListRent;
[Authorize(Roles = "Administrator")]
public record CreatePriceListRentCommand : IRequest<string>
{
    public string? NameRent { get; init; }
    public decimal PriceRentUnit { get; init; }
    public int Months { get; init; }
}

public class CreatePriceListRentCommandHandler : IRequestHandler<CreatePriceListRentCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreatePriceListRentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreatePriceListRentCommand request, CancellationToken cancellationToken)
    {
        var entity = new PriceListRent
        {
            NameRent = request.NameRent,
            PriceRentUnit = request.PriceRentUnit,
            Months = request.Months,
        };

        _context.PriceListRents.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
