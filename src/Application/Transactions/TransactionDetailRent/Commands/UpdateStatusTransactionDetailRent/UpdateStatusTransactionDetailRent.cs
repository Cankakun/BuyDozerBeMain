using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.UserEntitys.Commands.UpdateStatusTransactionDetailRent;
[Authorize(Roles = "Administrator")]

public record UpdateStatusTransactionDetailRentCommand : IRequest
{
    public required string Id { get; init; }
    public required int StatusTransaction { get; init; }
}

public class UpdateStatusTransactionDetailRentCommandHandler : IRequestHandler<UpdateStatusTransactionDetailRentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateStatusTransactionDetailRentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateStatusTransactionDetailRentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Transactions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.StatusTransaction = request.StatusTransaction;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
