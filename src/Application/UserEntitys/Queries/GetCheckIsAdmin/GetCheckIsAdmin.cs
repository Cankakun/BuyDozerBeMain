using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Common.Security;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;

[Authorize(Roles = "Administrator")]
public record GetCheckIsAdminsQuery : IRequest<Response>
{
    public required string email { get; init; }
};
public class GetCheckIsAdminsQueryHandler : IRequestHandler<GetCheckIsAdminsQuery, Response>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCheckIsAdminsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response> Handle(GetCheckIsAdminsQuery request, CancellationToken cancellationToken)
    {
        return new Response
        {
            Status = 200,
            Message = "success",
            Data = await _context.UserEntitys
                .AsNoTracking()
                .Where(x => x.Email == request.email)
                .ProjectTo<UserEntityDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.UserName)
                .ToListAsync(cancellationToken)
        };

    }
}
