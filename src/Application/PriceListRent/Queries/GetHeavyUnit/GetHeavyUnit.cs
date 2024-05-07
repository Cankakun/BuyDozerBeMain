using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;

namespace BuyDozerBeMain.Application.PriceListRents.Queries.GetPriceListRent;
public record GetPriceListRentsQuery : IRequest<PaginatedList<PriceListRentDTO>>
{
    public string? ParameterNameRent { get; init; }
    public bool SortPrice { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
};
public class GetPriceListRentsQueryHandler : IRequestHandler<GetPriceListRentsQuery, PaginatedList<PriceListRentDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPriceListRentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PriceListRentDTO>> Handle(GetPriceListRentsQuery request, CancellationToken cancellationToken)
    {
        return !request.SortPrice
                ? await _context.PriceListRents
                            .AsNoTracking()
                            .Where(x => EF.Functions.Like(x.NameRent, request.ParameterNameRent))
                            .ProjectTo<PriceListRentDTO>(_mapper.ConfigurationProvider)
                            .OrderByDescending(t => t.PriceRentUnit)
                            .PaginatedListAsync(request.PageNumber, request.PageSize)
                : await _context.PriceListRents
                            .AsNoTracking()
                            .Where(x => EF.Functions.Like(x.NameRent, request.ParameterNameRent))
                            .ProjectTo<PriceListRentDTO>(_mapper.ConfigurationProvider)
                            .OrderBy(t => t.PriceRentUnit)
                            .PaginatedListAsync(request.PageNumber, request.PageSize);


    }
}
