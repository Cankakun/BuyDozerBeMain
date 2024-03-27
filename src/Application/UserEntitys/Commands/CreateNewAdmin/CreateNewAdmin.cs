using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BuyDozerBeMain.Application.UserEntitys.Commands.CreateNewAdmin;

public record CreateNewAdminCommand : IRequest<string>
{
    public required string Id { get; init; }
}

public class CreateNewAdminCommandHandler : IRequestHandler<CreateNewAdminCommand, string>
{
    private readonly UserManager<UserEntity> _userManager;
    public CreateNewAdminCommandHandler(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(CreateNewAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, "administrator");
        }

        return request.Id;

    }
}