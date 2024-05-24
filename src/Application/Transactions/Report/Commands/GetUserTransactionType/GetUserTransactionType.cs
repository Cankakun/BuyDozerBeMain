using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;

namespace BuyDozerBeMain.Application.Transactions.Report.GetUnitRemaining;
[Authorize]
public record GetUserTransactionTypeQuery : IRequest<List<UserTransactionTypeDto>>;

public class GetUserTransactionTypeQueryHandler : IRequestHandler<GetUserTransactionTypeQuery, List<UserTransactionTypeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetUserTransactionTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserTransactionTypeDto>> Handle(GetUserTransactionTypeQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Transactions.GroupBy(x => x.UserId).CountAsync();
        var buy = from usr in _context.UserEntitys
                  join trx in _context.Transactions on usr.Id equals trx.UserId
                  join db in _context.DetailBuys on trx.Id equals db.TransactionId
                  where !_context.UserEntitys.Any(u =>
                      u.Id == usr.Id &&
                      _context.Transactions.Any(t => t.UserId == u.Id &&
                      _context.DetailRents.Any(dr => dr.TransactionId == t.Id)))
                  select usr;

        var rent = from usr in _context.UserEntitys
                   join trx in _context.Transactions on usr.Id equals trx.UserId
                   join dr in _context.DetailRents on trx.Id equals dr.TransactionId
                   where !_context.UserEntitys.Any(u =>
                       u.Id == usr.Id &&
                       _context.Transactions.Any(t => t.UserId == u.Id &&
                       _context.DetailBuys.Any(db => db.TransactionId == t.Id)))
                   select usr;

        // var both = _context.UserEntitys
        //         .Where(usr =>
        //             _context.UserEntitys.Any(u =>
        //                 _context.Transactions.Any(t => t.UserId == u.Id &&
        //                     _context.DetailRents.Any(dr => dr.TransactionId == t.Id))) &&
        //             _context.UserEntitys.Any(u =>
        //                 _context.Transactions.Any(t => t.UserId == u.Id &&
        //                     _context.DetailBuys.Any(db => db.TransactionId == t.Id))))
        //         .Count();

        var usersWithRent = _context.UserEntitys
                .Where(usr => _context.Transactions
                    .Any(t => t.UserId == usr.Id &&
                        _context.DetailRents.Any(dr => dr.TransactionId == t.Id)))
                .Select(usr => usr.Id)
                .ToList();

        var usersWithBuy = _context.UserEntitys
            .Where(usr => _context.Transactions
                .Any(t => t.UserId == usr.Id &&
                    _context.DetailBuys.Any(db => db.TransactionId == t.Id)))
            .Select(usr => usr.Id)
            .ToList();

        var both = usersWithRent.Intersect(usersWithBuy).Count();



        return
        [
            new(){
                BuyCount = buy.Count(),
                RentCount = rent.Count(),
                UserCount = user,
                BothCount= both,
            }
        ];
    }

}