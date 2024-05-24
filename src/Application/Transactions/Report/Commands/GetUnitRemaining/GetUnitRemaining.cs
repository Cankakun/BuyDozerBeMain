using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.Transactions.Report.GetUnitRemaining;
[Authorize]
public record GetUnitRemainingQuery : IRequest<UnitRemainingVm>;

public class GetUnitRemainingQueryHandler : IRequestHandler<GetUnitRemainingQuery, UnitRemainingVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUnitRemainingQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UnitRemainingVm> Handle(GetUnitRemainingQuery request, CancellationToken cancellationToken)
    {
        var buy = await _context.Transactions.Where(t => t.DetailBuy != null && t.DetailRents == null).SumAsync(t => t.QtyTransaction);
        var rent = await _context.Transactions.Where(t => t.DetailRents != null && t.DetailBuy == null).SumAsync(t => t.QtyTransaction);
        var free = await _context.HeavyUnits.SumAsync(t => t.QtyUnit);

        return new UnitRemainingVm
        {
            Items = [
                new() {
                    UnitBuyed = buy,
                    UnitRented = rent,
                    UnitFree = free
                }
            ]
            // return
            // UnitBuyed = buy,
            // UnitRented = rent,
            // UnitFree = free
        };
    }
}