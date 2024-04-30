using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.Transactions.Commands.DeleteTransaction;
[Authorize(Roles = "Administrator")]

public record DeleteTransactionCommand(string Id) : IRequest;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        // var detail = await _context.DetailRents
        //     .Where(l => l.TransactionId == request.Id)
        //     .SingleOrDefaultAsync(cancellationToken);

        // Guard.Against.NotFound(request.Id, detail);

        // _context.DetailRents.Remove(detail);

        // await _context.SaveChangesAsync(cancellationToken);

        var master = await _context.Transactions
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, master);

        _context.Transactions.Remove(master);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
