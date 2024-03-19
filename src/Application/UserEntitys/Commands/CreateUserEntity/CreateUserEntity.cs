// using BuyDozerBeMain.Application.Common.Interfaces;
// using BuyDozerBeMain.Application.Common.Security;
// using BuyDozerBeMain.Domain.Entities;
// using Microsoft.AspNetCore.Identity;

// namespace BuyDozerBeMain.Application.UserEntitys.Commands.CreateUserEntity;
// public record CreateUserEntityCommand : IRequest<string>
// {
//     public string? UserName { get; init; }
//     public string? Email { get; init; }
//     public required string Password { get; init; }
//     public string? CompanyUser { get; init; }
//     public string? PositionUser { get; init; }
// }

// public class CreateUserEntityCommandHandler : IRequestHandler<CreateUserEntityCommand, string>
// {
//     private readonly UserManager<UserEntity> _userManager;

//     public CreateUserEntityCommandHandler(UserManager<UserEntity> userManager)
//     {
//         _userManager = userManager;
//     }

//     public async Task<string> Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
//     {
//         var entity = new UserEntity
//         {
//             UserName = request.UserName,
//             Email = request.Email,
//             CompanyUser = request.CompanyUser,
//             PositionUser = request.PositionUser,

//         };
//         await _userManager.CreateAsync(entity, request.Password);
//         return entity.Id;
//     }
// }
