using System.ComponentModel;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.HeavyUnits.Queries.GetHeavyUnit;

[Authorize]
public record GetHeavyUnitsQuery : IRequest<PaginatedList<HeavyUnitDTO>>
{
    public string? ParameterUnit { get; init; }
    public bool PriceRent { get; init; } = false;
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
        if (request.PriceRent && request.PriceBuy)
        {
            return await _context.HeavyUnits
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
                .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.PriceRentUnit)
                .OrderBy(t => t.PriceBuyUnit)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

        }
        else if (!request.PriceRent && !request.PriceBuy)
        {
            return await _context.HeavyUnits
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
                .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(t => t.PriceRentUnit)
                .OrderByDescending(t => t.PriceBuyUnit)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.PriceRent && !request.PriceBuy)
        {
            return await _context.HeavyUnits
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
                .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.PriceRentUnit)
                .OrderByDescending(t => t.PriceBuyUnit)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (!request.PriceRent && request.PriceBuy)
        {
            return await _context.HeavyUnits
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
                .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(t => t.PriceRentUnit)
                .OrderBy(t => t.PriceBuyUnit)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            return await _context.HeavyUnits
            .AsNoTracking()
            .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
            .ProjectTo<HeavyUnitDTO>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.NameUnit)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        }

    }
}
