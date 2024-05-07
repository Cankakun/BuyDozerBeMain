using System.Net.Http.Headers;
using BuyDozerBeMain.Domain.Entities;
using BuyDozerBeMain.Application.PriceListRents.Commands.CreatePriceListRent;
using BuyDozerBeMain.Application.PriceListRents.Commands.UpdatePriceListRent;
using System.Text.Json;
using BuyDozerBeMain.Application.PriceListRents.Commands.DeletePriceListRent;
using BuyDozerBeMain.Application.PriceListRents.Queries.GetPriceListRent;
using BuyDozerBeMain.Application.Common.Models;

namespace BuyDozerBeMain.Web.Endpoints;

public class PriceListRents : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAllPriceListRent, "GetPriceListRent")
            .MapPost(CreatePriceListRent, "CreatePriceListRent")
            .MapPut(UpdatePriceListRent, "UpdatePriceListRent/{id}")
            .MapDelete(DeletePriceListRent, "DeletePriceListRent/{id}");
    }

    public async Task<PaginatedList<PriceListRentDTO>> GetAllPriceListRent(ISender sender, [AsParameters] GetPriceListRentsQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<IResult> CreatePriceListRent(ISender sender, CreatePriceListRentCommand command)
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

    public async Task<IResult> UpdatePriceListRent(ISender sender, string id, UpdatePriceListRentCommand command)
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

    public async Task<IResult> DeletePriceListRent(ISender sender, string id)
    {
        await sender.Send(new DeletePriceListRentCommand(id));
        return Results.NoContent();
    }
}
