using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.UserEntitys.Commands.UpdateUserEntity;

public record UpdateUserEntityCommand : IRequest
{
    public required string Id { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? CompanyUser { get; init; }
    public string? PositionUser { get; init; }
}

public class UpdateUserEntityCommandHandler : IRequestHandler<UpdateUserEntityCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserEntityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateUserEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserEntitys
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.UserName = request.UserName;
        entity.Email = request.Email;
        entity.CompanyUser = request.CompanyUser;
        entity.PositionUser = request.PositionUser;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
