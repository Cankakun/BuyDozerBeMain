using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.UserEntitys.Commands.DeleteUserEntity;
[Authorize(Roles = "Administrator")]

public record DeleteUserEntityCommand(string Id) : IRequest;

public class DeleteUserEntityCommandHandler : IRequestHandler<DeleteUserEntityCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserEntityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserEntitys
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.UserEntitys.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
