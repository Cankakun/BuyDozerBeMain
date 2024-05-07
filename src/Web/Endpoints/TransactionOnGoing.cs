
using System.Net.Http.Headers;
using System.Text.Json;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Transactions.Commands.UpdatePaymentConfirmationReceiptTransaction;
using BuyDozerBeMain.Application.Transactions.TransactionOnGoing.Queries.GetTransactionOnGoing;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuyDozerBeMain.Web.Endpoints;

public class TransactionOnGoing : EndpointGroupBase
{
    // private readonly IApplicationDbContext _context;
    // public TransactionDetailRBuyTransactionDetailBuy(IApplicationDbContext context)
    // {
    //     _context = context;
    // }

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTransactionOnGoing, "GetTransactionOnGoing")
            .MapPut(UpdatePaymentConfirmationReceiptTransaction, "UpdateTransactionOnGoing/{id}");
    }

    public async Task<PaginatedList<TransactionOnGoingDTO>> GetTransactionOnGoing(ISender sender, [AsParameters] GetTransactionOnGoing query)
    {
        return await sender.Send(query);
    }
    public async Task<IResult> UpdatePaymentConfirmationReceiptTransaction(ISender sender, string id, UpdatePaymentConfirmationReceiptTransactionCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        return Results.Content(JsonSerializer.Serialize(await sender.Send(command)), mediaType.MediaType);
    }
}
