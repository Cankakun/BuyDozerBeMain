using System.Net.Http.Headers;
using System.Text.Json;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.UserEntitys.Commands.CreateNewAdmin;
using BuyDozerBeMain.Application.UserEntitys.Commands.DeleteUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Commands.LoginUserEntity;


// using BuyDozerBeMain.Application.UserEntitys.Commands.CreateUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Commands.UpdateUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.Net.Http.Headers;

namespace BuyDozerBeMain.Web.Endpoints;

public class UserEntitys : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAllUserEntity)
            .MapGet(GetIsAdmin, "isAdmin")
            .MapPost(CreateNewAdmin, "NewAdmin")
            .MapPost(LoginUserEntity, "CustomLogin")
            .MapPut(UpdateUserEntity, "UpdateCompanyAndPositionUser/{id}")
            .MapDelete(DeleteUserEntity, "{id}");
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
    public async Task<IResult> CreateNewAdmin(ISender sender, string id, CreateNewAdminCommand command)
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
    public async Task<IResult> LoginUserEntity(ISender sender, [FromBody] LoginUserEntityCommand command)
    {
        var result = await sender.Send(command);
        // MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        // Response response = new Response
        // {
        //     Status = 200,
        //     Message = "success",
        //     Data = result
        // };
        // if (id != command.Id) return Results.BadRequest();
        return Results.Content(result, "application/json");
    }

    public async Task<PaginatedList<UserEntityDTO>> GetAllUserEntity(ISender sender, [AsParameters] GetUserEntitysQuery query)
    {
        return await sender.Send(query);
    }

    public Task<Response> GetIsAdmin(ISender sender, [AsParameters] GetCheckIsAdminsQuery query)
    {
        return sender.Send(query);
    }

    public async Task<IResult> DeleteUserEntity(ISender sender, string id)
    {
        await sender.Send(new DeleteUserEntityCommand(id));
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        Response response = new Response
        {
            Status = 200,
            Message = "success",
            Data = "Data dengan ID " + id + " sukses dihapus"
        };
        return Results.Content(JsonSerializer.Serialize(response), mediaType.MediaType);
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
