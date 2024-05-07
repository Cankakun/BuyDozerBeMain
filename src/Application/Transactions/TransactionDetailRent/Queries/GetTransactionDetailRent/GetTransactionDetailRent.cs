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
    public string? ParameterUserName { get; init; }
    public string? ParameterTransactionNumber { get; init; }
    public bool SortDate { get; init; } = false;
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
        return request.SortDate ?
            await _context.Transactions
                .AsNoTracking()
                .Include(a => a.DetailRents)
                .Include(a => a.Unit)
                .Include(a => a.User)
                .Where(x => EF.Functions.Like(x.User.UserName, request.ParameterUserName) && x.DetailBuy == null || EF.Functions.Like(x.TransactionNum, request.ParameterTransactionNumber) && x.DetailBuy == null)
                .OrderBy(a => a.Created)
                .ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize)
                :
            await _context.Transactions
                .AsNoTracking()
                .Include(a => a.DetailRents)
                .Include(a => a.Unit)
                .Include(a => a.User)
                .Where(x => EF.Functions.Like(x.User.UserName, request.ParameterUserName) && x.DetailBuy == null || EF.Functions.Like(x.TransactionNum, request.ParameterTransactionNumber) && x.DetailBuy == null)
                .OrderByDescending(a => a.Created)
                .ProjectTo<TransactionDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
