using System.Net.Http.Headers;
using BuyDozerBeMain.Domain.Entities;
using System.Text.Json;
using BuyDozerBeMain.Application.Transactions.TransactionDetailRent.Commands.CreateTransactionDetailRent;
using BuyDozerBeMain.Application.Transactions.TransactionDetailRent.Commands.DeleteTransactionDetailRent;
using BuyDozerBeMain.Application.UserEntitys.Commands.UpdateStatusTransactionDetailRent;

namespace BuyDozerBeMain.Web.Endpoints;

public class TransactionDetailRents : EndpointGroupBase
{
    // private readonly IApplicationDbContext _context;
    // public TransactionDetailRents(IApplicationDbContext context)
    // {
    //     _context = context;
    // }

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateTransactionDetailRent, "CreateTransactionDetailRent")
            .MapPut(UpdateTransactionDetailRent, "UpdateTransactionDetailRent/{id}")
            .MapDelete(DeleteTransactionDetailRent, "DeleteTransactionDetailRent/{id}");
    }



    public async Task<IResult> CreateTransactionDetailRent(ISender sender, CreateTransactionDetailRentCommand command)
    {
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        Response response = new Response
        {
            Status = 200,
            Message = "success",
            Data = command
        };
        await sender.Send(command);
        return Results.Content(JsonSerializer.Serialize(response), mediaType.MediaType);
    }
    public async Task<IResult> UpdateTransactionDetailRent(ISender sender, string id, UpdateStatusTransactionDetailRentCommand command)
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
    public async Task<IResult> DeleteTransactionDetailRent(ISender sender, string id)
    {
        await sender.Send(new DeleteTransactionDetailRentCommand(id));
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        Response response = new Response
        {
            Status = 200,
            Message = "success",
            Data = "Transaksi dengan ID " + id + " sukses dihapus!"
        };
        return Results.Content(JsonSerializer.Serialize(response), mediaType.MediaType);
    }

}
