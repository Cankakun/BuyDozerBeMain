using System.Text.Json;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Mappings;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Common.Security;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace BuyDozerBeMain.Application.Transactions.TransactionOnGoing.Queries.GetTransactionOnGoing;
[Authorize]

public record GetTransactionOnGoing : IRequest<PaginatedList<TransactionOnGoingDTO>>
{
    public string? ParameterUserName { get; init; }
    public string? ParameterTransactionNumber { get; init; }
    public string? ParameterStatus { get; init; }
    public bool SortDate { get; init; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
};
public class GetTransactionOnGoingHandler : IRequestHandler<GetTransactionOnGoing, PaginatedList<TransactionOnGoingDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionOnGoingHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TransactionOnGoingDTO>> Handle(GetTransactionOnGoing request, CancellationToken cancellationToken)
    {
        var result =
                request.ParameterUserName != null && request.ParameterTransactionNumber != null && request.ParameterTransactionNumber != request.ParameterUserName ?
                await _context.Transactions
                    .AsNoTracking()
                    .Where(x => EF.Functions.Like(x.TransactionNum, request.ParameterTransactionNumber) && EF.Functions.Like(x.StatusTransaction.ToString(), request.ParameterStatus) && EF.Functions.Like(x.User.UserName, request.ParameterUserName))
                    .ProjectTo<TransactionOnGoingDTO>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize) :

                request.SortDate ?
                await _context.Transactions
                    .AsNoTracking()
                    .Where(x => EF.Functions.Like(x.User.UserName, request.ParameterUserName) && EF.Functions.Like(x.StatusTransaction.ToString(), request.ParameterStatus) || EF.Functions.Like(x.TransactionNum, request.ParameterTransactionNumber) && EF.Functions.Like(x.StatusTransaction.ToString(), request.ParameterStatus))
                    .OrderBy(a => a.Created)
                    .ProjectTo<TransactionOnGoingDTO>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize) :

                await _context.Transactions
                    .AsNoTracking()
                    .Where(x => EF.Functions.Like(x.User.UserName, request.ParameterUserName) && EF.Functions.Like(x.StatusTransaction.ToString(), request.ParameterStatus) || EF.Functions.Like(x.TransactionNum, request.ParameterTransactionNumber) && EF.Functions.Like(x.StatusTransaction.ToString(), request.ParameterStatus))
                    .OrderByDescending(a => a.Created)
                    .ProjectTo<TransactionOnGoingDTO>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);

        return result;
    }
}
