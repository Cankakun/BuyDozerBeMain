using System.Net.Http.Headers;
using System.Text.Json;
// using BuyDozerBeMain.Application.UserEntitys.Commands.CreateUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Commands.UpdateUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;
using BuyDozerBeMain.Domain.Entities;
// using Microsoft.Net.Http.Headers;

namespace BuyDozerBeMain.Web.Endpoints;

public class UserEntitys : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAllUserEntity)
            // .MapPost(CreateUserEntity)
            .MapPut(UpdateUserEntity, "UpdateCompanyAndPositionUser/{id}");
    }

    public async Task<IResult> UpdateUserEntity(ISender sender, string id, UpdateUserEntityCommand command)
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

    public async Task<UserEntityVm> GetAllUserEntity(ISender sender)
    {
        return await sender.Send(new GetUserEntitysQuery());
    }

    // public async Task<IResult> CreateUserEntity(ISender sender, CreateUserEntityCommand command)
    // {
    //     MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
    //     Response response = new Response
    //     {
    //         Status = 200,
    //         Message = "success",
    //         Data = command
    //     };
    //     await sender.Send(command);
    //     return Results.Content(JsonSerializer.Serialize(response), mediaType.MediaType);
    // }
}
