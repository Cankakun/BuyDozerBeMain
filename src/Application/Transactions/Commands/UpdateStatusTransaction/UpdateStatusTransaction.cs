using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.Transactions.Commands.UpdateStatusTransaction;
[Authorize(Roles = "Administrator")]

public record UpdateStatusTransactionCommand : IRequest
{
    public required string Id { get; init; }
    public required int StatusTransaction { get; init; }
}

public class UpdateStatusTransactionCommandHandler : IRequestHandler<UpdateStatusTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateStatusTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateStatusTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Transactions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.StatusTransaction = request.StatusTransaction;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
