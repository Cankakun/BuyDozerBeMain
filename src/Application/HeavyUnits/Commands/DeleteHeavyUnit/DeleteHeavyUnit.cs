using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.HeavyUnits.Commands.DeleteHeavyUnit;

public record DeleteHeavyUnitCommand(int Id) : IRequest;

public class DeleteHeavyUnitCommandHandler : IRequestHandler<DeleteHeavyUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteHeavyUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteHeavyUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.HeavyUnits
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.HeavyUnits.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
