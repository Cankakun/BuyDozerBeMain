using System.Net.Http.Headers;
using BuyDozerBeMain.Domain.Entities;
using System.Text.Json;
using BuyDozerBeMain.Application.Transactions.TransactionDetailRent.Commands.CreateTransactionDetailRent;

namespace BuyDozerBeMain.Web.Endpoints;

public class TransactionDetailRents : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateTransactionDetailRent);
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

}
