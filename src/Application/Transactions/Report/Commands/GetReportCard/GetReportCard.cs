using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.Transactions.Report.ReportCard;
[Authorize(Roles = "Administrator")]
public record GetReportCardQuery : IRequest<List<ReportCardDto>>;

public class GetReportCardQueryHandler : IRequestHandler<GetReportCardQuery, List<ReportCardDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReportCardQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReportCardDto>> Handle(GetReportCardQuery request, CancellationToken cancellationToken)
    {

        var buy = await _context.Transactions.Where(t => t.DetailBuy != null && t.DetailRents == null).CountAsync();
        var rent = await _context.Transactions.Where(t => t.DetailRents != null && t.DetailBuy == null).CountAsync();
        var unit = await _context.HeavyUnits.CountAsync();
        var user = await _context.UserEntitys.CountAsync();

        return [
            new(){
                BuyTransactionCount = buy,
                RentTransactionCount = rent,
                UnitCount = unit,
                UserCount = user
            }
        ];
    }
}