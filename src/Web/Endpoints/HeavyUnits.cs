using System.Net.Http.Headers;
using BuyDozerBeMain.Domain.Entities;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;
using BuyDozerBeMain.Application.HeavyUnits.Commands.UpdateHeavyUnit;
using System.Text.Json;
using BuyDozerBeMain.Application.HeavyUnits.Commands.DeleteHeavyUnit;

namespace BuyDozerBeMain.Web.Endpoints;

public class HeavyUnits : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            // .MapGet(GetAllHeavyUnit)
            .MapPost(CreateHeavyUnit)
            .MapPut(UpdateHeavyUnit, "{id}")
            .MapDelete(DeleteHeavyUnit, "{id}");
    }

    // public async Task<HeavyUnitVm> GetAllHeavyUnit(ISender sender)
    // {
    //     return await sender.Send(new GetHeavyUnitsQuery());
    // }

    public async Task<IResult> CreateHeavyUnit(ISender sender, CreateHeavyUnitCommand command)
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

    public async Task<IResult> UpdateHeavyUnit(ISender sender, int id, UpdateHeavyUnitCommand command)
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

    public async Task<IResult> DeleteHeavyUnit(ISender sender, int id)
    {
        await sender.Send(new DeleteHeavyUnitCommand(id));
        return Results.NoContent();
    }
}
