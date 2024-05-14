using System.Net.Http.Headers;
using BuyDozerBeMain.Domain.Entities;
using System.Text.Json;
using BuyDozerBeMain.Application.Transactions.TransactionDetailBuy.Commands.CreateTransactionDetailBuy;
using BuyDozerBeMain.Application.Transactions.Commands.DeleteTransaction;
// using BuyDozerBeMain.Application.Transactions.Queries.GetTransaction;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.Transactions.TransactionDetailBuy.Queries.GetTransactionDetailBuy;
using BuyDozerBeMain.Application.Transactions.Commands.UpdateStatusTransaction;
using System.Text.Json.Serialization;

namespace BuyDozerBeMain.Web.Endpoints;

public class TransactionDetailBuy : EndpointGroupBase
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
            .MapGet(GetTransactionDetailBuy, "GetTransactionDetailBuy")
            .MapPost(CreateTransactionDetailBuy, "CreateTransactionDetailBuy")
            .MapPut(UpdateTransactionDetailBuy, "UpdateTransactionDetailBuy/{id}")
            .MapDelete(DeleteTransaction, "DeleteTransactionDetailBuy/{id}");
    }



    public async Task<IResult> CreateTransactionDetailBuy(ISender sender, CreateTransactionDetailBuyCommand command)
    {
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        var response = await sender.Send(command);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
        };
        return Results.Content(JsonSerializer.Serialize(response, options), mediaType.MediaType);
    }
    public async Task<IResult> UpdateTransactionDetailBuy(ISender sender, string id, UpdateStatusTransactionCommand command)
    {
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        Response response = new Response
        {
            Status = 200,
            Message = "success",
            Data = command
        };
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.Content(JsonSerializer.Serialize(response), mediaType.MediaType);
    }
    public async Task<IResult> DeleteTransaction(ISender sender, string id)
    {
        await sender.Send(new DeleteTransactionCommand(id));
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        Response response = new Response
        {
            Status = 200,
            Message = "success",
            Data = "Transaksi dengan ID " + id + " sukses dihapus!"
        };
        return Results.Content(JsonSerializer.Serialize(response), mediaType.MediaType);
    }
    public async Task<PaginatedList<TransactionDTO>> GetTransactionDetailBuy(ISender sender, [AsParameters] GetTransactionDetailBuy query)
    {
        return await sender.Send(query);
    }

}
