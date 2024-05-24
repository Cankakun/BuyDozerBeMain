using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.Transactions.Report.TransactionReport;
[Authorize]
public record GetTransactionReportQuery : IRequest<List<TransactionReportDto>>;

public class GetTransactionReportQueryHandler : IRequestHandler<GetTransactionReportQuery, List<TransactionReportDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionReportQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TransactionReportDto>> Handle(GetTransactionReportQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Transactions
            .GroupBy(t => t.DateTransaction.Month)
            .Select(g => new TransactionReportDto
            {   
                MonthTransaction = g.Key,
                TransaksiSewa = g.Select(t => t.DetailRents).Where(t => t.TransactionId != null).Count(),
                TransaksiBeli = g.Select(t => t.DetailBuy).Where(t => t.TransactionId != null).Count(),
                Transaksi = g.Count()
            })
            .OrderBy(dto => dto.MonthTransaction)
            .ToListAsync(cancellationToken);

        return result;

    }
}