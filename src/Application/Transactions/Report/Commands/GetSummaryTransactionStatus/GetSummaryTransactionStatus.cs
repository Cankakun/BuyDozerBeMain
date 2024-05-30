using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.Transactions.Report.SummaryTransactionStatus;
[Authorize(Roles = "Administrator")]
public record GetSummaryTransactionStatusQuery : IRequest<List<SummaryTransactionStatusDto>>;

public class GetSummaryTransactionStatusQueryHandler : IRequestHandler<GetSummaryTransactionStatusQuery, List<SummaryTransactionStatusDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSummaryTransactionStatusQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SummaryTransactionStatusDto>> Handle(GetSummaryTransactionStatusQuery request, CancellationToken cancellationToken)
    {
        var rejected = await _context.Transactions
            .Where(x => x.StatusTransaction == 0)
            .CountAsync();
        var onGoing = await _context.Transactions
            .Where(x => x.StatusTransaction == 1)
            .CountAsync();
        var paid = await _context.Transactions
            .Where(x => x.StatusTransaction == 2)
            .CountAsync();
        var finish = await _context.Transactions
            .Where(x => x.StatusTransaction == 3)
            .CountAsync();

        return [
            new(){
                TransactionRejected = rejected,
                TransactionOnGoing = onGoing,
                TransactionPaid =paid,
                TransactionFinish = finish
            }
        ];

    }
}