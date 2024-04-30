using System.Text.Json;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailBuy.Queries.GetTransactionDetailBuy;

public record GetTransactionDetailBuy : IRequest<PaginatedList<TransactionDTO>>
{
    public string? ParameterUserName { get; init; }
    public string? ParameterTransactionNumber { get; init; }
    public bool SortDate { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
};
public class GetTransactionDetailBuyHandler : IRequestHandler<GetTransactionDetailBuy, PaginatedList<TransactionDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionDetailBuyHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TransactionDTO>> Handle(GetTransactionDetailBuy request, CancellationToken cancellationToken)
    {
        return request.SortDate ?
            await _context.Transactions
                .AsNoTracking()
                .Include(a => a.DetailRents)
                .Include(a => a.Unit)
                .Include(a => a.User)
                .Where(x => EF.Functions.Like(x.User.UserName, request.ParameterUserName) || EF.Functions.Like(x.TransactionNum, request.ParameterTransactionNumber))
                .OrderBy(a => a.DateTransaction)
                .ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize) :
            await _context.Transactions
                .AsNoTracking()
                .Include(a => a.DetailRents)
                .Include(a => a.Unit)
                .Include(a => a.User)
                .Where(x => EF.Functions.Like(x.User.UserName, request.ParameterUserName) || EF.Functions.Like(x.TransactionNum, request.ParameterTransactionNumber))
                .OrderByDescending(a => a.DateTransaction)
                .ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

    }
}
