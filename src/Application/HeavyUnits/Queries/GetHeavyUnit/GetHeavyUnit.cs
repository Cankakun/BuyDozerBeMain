using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.HeavyUnits.Queries.GetHeavyUnit;

[Authorize]
public record GetHeavyUnitsQuery : IRequest<HeavyUnitVm>;
public class GetHeavyUnitsQueryHandler : IRequestHandler<GetHeavyUnitsQuery, HeavyUnitVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetHeavyUnitsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HeavyUnitVm> Handle(GetHeavyUnitsQuery request, CancellationToken cancellationToken)
    {
        return new HeavyUnitVm
        {
            Data = await _context.HeavyUnits
                .AsNoTracking()
                .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.NameUnit)
                .ToListAsync(cancellationToken)
        };
    }
}
