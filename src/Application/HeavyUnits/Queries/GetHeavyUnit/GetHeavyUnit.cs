using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;

namespace BuyDozerBeMain.Application.HeavyUnits.Queries.GetHeavyUnit;
public record GetHeavyUnitsQuery : IRequest<PaginatedList<HeavyUnitDTO>>
{
    public string? ParameterUnit { get; init; }
    public bool PriceBuy { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
};
public class GetHeavyUnitsQueryHandler : IRequestHandler<GetHeavyUnitsQuery, PaginatedList<HeavyUnitDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetHeavyUnitsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<HeavyUnitDTO>> Handle(GetHeavyUnitsQuery request, CancellationToken cancellationToken)
    {
        return !request.PriceBuy
                ? await _context.HeavyUnits
                            .AsNoTracking()
                            .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
                            .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
                            .OrderByDescending(t => t.PriceBuyUnit)
                            .PaginatedListAsync(request.PageNumber, request.PageSize)
                : await _context.HeavyUnits
                            .AsNoTracking()
                            .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
                            .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
                            .OrderBy(t => t.PriceBuyUnit)
                            .PaginatedListAsync(request.PageNumber, request.PageSize);


    }
}
