using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;

[Authorize]
public record GetUserEntitysQuery : IRequest<UserEntityVm>;
public class GetUserEntitysQueryHandler : IRequestHandler<GetUserEntitysQuery, UserEntityVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserEntitysQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserEntityVm> Handle(GetUserEntitysQuery request, CancellationToken cancellationToken)
    {
        return new UserEntityVm
        {
            Data = await _context.UserEntitys
                .AsNoTracking()
                .ProjectTo<UserEntityDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.UserName)
                .ToListAsync(cancellationToken)
        };
    }
}
