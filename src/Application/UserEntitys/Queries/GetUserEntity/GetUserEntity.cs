using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Common.Security;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;

[Authorize(Roles = "Administrator")]
public record GetUserEntitysQuery : IRequest<PaginatedList<UserEntityDTO>>
{
    public string? ParameterName { get; init; }
    public bool SortUserName { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
};
public class GetUserEntitysQueryHandler : IRequestHandler<GetUserEntitysQuery, PaginatedList<UserEntityDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<UserEntity> _user;

    public GetUserEntitysQueryHandler(IApplicationDbContext context, UserManager<UserEntity> user)
    {
        _context = context;
        _user = user;
    }

    public async Task<PaginatedList<UserEntityDTO>> Handle(GetUserEntitysQuery request, CancellationToken cancellationToken)
    {
        // .PaginatedListAsync(request.PageNumber, request.PageSize);
        var users = await _context.UserEntitys
                .AsNoTracking()
                // .ProjectTo<UserEntityDTO>(_mapper.ConfigurationProvider)
                .Where(x => EF.Functions.Like(x.UserName, request.ParameterName))
                .ToListAsync();
        var querys = request.SortUserName ? users.OrderBy(a => a.UserName).Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize) : users.OrderByDescending(a => a.UserName).Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        var userEntities = new List<UserEntityDTO>();

        foreach (var user in querys)
        {
            var isAdmin = await _user.IsInRoleAsync(user, "Administrator");
            userEntities.Add(new UserEntityDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CompanyUser = user.CompanyUser,
                PositionUser = user.PositionUser,
                IsAdmin = isAdmin
            }
            );
        }
        var totalUsers = await _user.Users.CountAsync();
        return new PaginatedList<UserEntityDTO>(userEntities, totalUsers, request.PageNumber, request.PageSize);
    }
}
