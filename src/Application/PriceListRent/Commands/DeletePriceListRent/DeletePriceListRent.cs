using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.PriceListRents.Commands.DeletePriceListRent;
[Authorize(Roles = "Administrator")]

public record DeletePriceListRentCommand(string Id) : IRequest;

public class DeletePriceListRentCommandHandler : IRequestHandler<DeletePriceListRentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePriceListRentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePriceListRentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PriceListRents
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.PriceListRents.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
