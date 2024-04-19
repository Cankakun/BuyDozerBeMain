using System.Text.Json;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailRent.Queries.GetTransactionDetailRent;

public record GetTransactionDetailRent : IRequest<PaginatedList<TransactionDTO>>
{
    // public string? ParameterUserName { get; init; }
    // public bool SortDate { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
};
public class GetTransactionDetailRentHandler : IRequestHandler<GetTransactionDetailRent, PaginatedList<TransactionDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionDetailRentHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TransactionDTO>> Handle(GetTransactionDetailRent request, CancellationToken cancellationToken)
    {
        return await _context.Transactions
                            .AsNoTracking()
                            .Include(a => a.DetailRents)
                            .Include(a => a.Unit)
                            .Include(a => a.User)
                            // .Where(x => EF.Functions.Like(x.NameUnit, request.ParameterUnit))
                            .OrderBy(a => a.Id)
                            .ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)
                            .PaginatedListAsync(request.PageNumber, request.PageSize);
        // var transactionWithDetail = await _context.DetailRents.FindAsync(transaction.)
        // .Include(a => a.Transaction)
        // .ToListAsync();

        // var transactionWithDetail = transaction.GroupBy(a => a.TransactionId)
        // .Select(group => new GetTransactionDTO
        // {
        //     TransactionId = group.Key,
        //     TransactionNum = group.First().Transaction.TransactionNum,
        //     UserName = group.First().Transaction.User.UserName,
        //     NameUnit = group.First().Transaction.Unit.NameUnit,
        //     ReceiverName = group.First().Transaction.ReceiverName,
        //     ReceiverAddress = group.First().Transaction.ReceiverAddress,
        //     ReceiverHp = group.First().Transaction.ReceiverHp,
        //     QtyTransaction = group.First().Transaction.QtyTransaction,
        //     TotalPriceTransaction = group.First().Transaction.TotalPriceTransaction,
        //     DateTransaction = group.First().Transaction.DateTransaction,
        //     StatusTransaction = group.First().Transaction.StatusTransaction,
        //     DetailRentsId = group.First().Transaction.DetailRents.Id,
        //     DateRent = group.First().Transaction.DetailRents.DateRent,
        //     DateReturn = group.First().Transaction.DetailRents.DateReturn
        // }).ToList();

        // var totalTransaksi = await _context.Transactions.CountAsync();
        // return new PaginatedList<GetTransactionDTO>(transactionWithDetail, totalTransaksi, request.PageNumber, request.PageSize);
        // .ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)

    }
}
