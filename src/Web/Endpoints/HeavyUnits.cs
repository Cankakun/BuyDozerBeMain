using System.Net.Http.Headers;
using BuyDozerBeMain.Domain.Entities;
using BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;
using BuyDozerBeMain.Application.HeavyUnits.Commands.UpdateHeavyUnit;
using System.Text.Json;
using BuyDozerBeMain.Application.HeavyUnits.Commands.DeleteHeavyUnit;
using BuyDozerBeMain.Application.HeavyUnits.Queries.GetHeavyUnit;
using BuyDozerBeMain.Application.Common.Models;

namespace BuyDozerBeMain.Web.Endpoints;

public class HeavyUnits : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAllHeavyUnit, "GetHeavyUnit")
            .MapPost(CreateHeavyUnit, "CreateHeavyUnit")
            .MapPut(UpdateHeavyUnit, "UpdateHeavyUnit/{id}")
            .MapDelete(DeleteHeavyUnit, "DeleteHeavyUnit/{id}");
    }

    public async Task<PaginatedList<HeavyUnitDTO>> GetAllHeavyUnit(ISender sender, [AsParameters] GetHeavyUnitsQuery query)
    {
        return await sender.Send(query);
    }

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

    public async Task<IResult> UpdateHeavyUnit(ISender sender, string id, UpdateHeavyUnitCommand command)
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

    public async Task<IResult> DeleteHeavyUnit(ISender sender, string id)
    {
        await sender.Send(new DeleteHeavyUnitCommand(id));
        return Results.NoContent();
    }
}
