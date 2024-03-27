
using System.Text.Json;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Identity;
namespace BuyDozerBeMain.Application.UserEntitys.Commands.RegisterUserEntity;

public record RegisterUserEntityCommand : IRequest<string>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string UserName { get; init; }
    public required string CompanyUser { get; init; }
    public required string PositionUser { get; init; }
}

public class RegisterUserEntityCommandHandler : IRequestHandler<RegisterUserEntityCommand, string>
{
    private readonly UserManager<UserEntity> _userManager;
    public RegisterUserEntityCommandHandler(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(RegisterUserEntityCommand request, CancellationToken cancellationToken)
    {

        var user = new UserEntity
        {
            Email = request.Email,
            UserName = request.UserName,
            CompanyUser = request.CompanyUser,
            PositionUser = request.PositionUser
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            var response = new Response
            {
                Status = 200,
                Message = "success",
                Data = await _userManager.FindByEmailAsync(request.Email)
            };
            return JsonSerializer.Serialize(response);
        }
        else
        {
            var response = new Response
            {
                Status = 400,
                Message = "Bad Request",
                Data = result
            };
            return JsonSerializer.Serialize(response);
        }
    }
}