using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BuyDozerBeMain.Application.UserEntitys.Commands.DeleteAdmin;

public record DeleteAdminCommand(string Id) : IRequest;

public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand>
{
    private readonly UserManager<UserEntity> _userManager;

    public DeleteAdminCommandHandler(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        Guard.Against.NotFound(request.Id, user);
        if (user != null)
        {
            await _userManager.RemoveFromRoleAsync(user, "administrator");
        }

    }
}

