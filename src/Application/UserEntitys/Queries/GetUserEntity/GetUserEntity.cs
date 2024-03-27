using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;

[Authorize(Roles = "Administrator")]
public record GetUserEntitysQuery : IRequest<PaginatedList<UserEntityDTO>>
{
    public string? ParameterName { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
};
public class GetUserEntitysQueryHandler : IRequestHandler<GetUserEntitysQuery, PaginatedList<UserEntityDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserEntitysQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserEntityDTO>> Handle(GetUserEntitysQuery request, CancellationToken cancellationToken)
    {
        return await _context.UserEntitys
                .AsNoTracking()
                .ProjectTo<UserEntityDTO>(_mapper.ConfigurationProvider)
                .Where(x => EF.Functions.Like(x.UserName, request.ParameterName))
                .OrderBy(t => t.UserName)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
