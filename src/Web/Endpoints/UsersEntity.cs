using System.Net.Http.Headers;
using System.Text.Json;
using BuyDozerBeMain.Application.Common.Models;
using BuyDozerBeMain.Application.UserEntitys.Commands.CreateNewAdmin;
using BuyDozerBeMain.Application.UserEntitys.Commands.DeleteAdmin;
using BuyDozerBeMain.Application.UserEntitys.Commands.DeleteUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Commands.LoginUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Commands.RegisterUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Commands.UpdateUserEntity;
using BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuyDozerBeMain.Web.Endpoints;

public class UserEntitys : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAllUserEntity)
            .MapPost(CreateNewAdmin, "NewAdmin")
            .MapPost(RegisterUserEntity, "Register")
            .MapPost(LoginUserEntity, "Login")
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
        return Results.Content(result, "application/json");
    }
    public async Task<IResult> RegisterUserEntity(ISender sender, [FromBody] RegisterUserEntityCommand command)
    {
        var result = await sender.Send(command);
        return Results.Content(result, "application/json");
    }

    public async Task<PaginatedList<UserEntityDTO>> GetAllUserEntity(ISender sender, [AsParameters] GetUserEntitysQuery query)
    {
        return await sender.Send(query);
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
    public async Task<IResult> DeleteAdmin(ISender sender, string id)
    {
        await sender.Send(new DeleteAdminCommand(id));
        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        Response response = new Response
        {
            Status = 200,
            Message = "success",
            Data = "Data dengan ID " + id + " sukses diubah"
        };
        return Results.Content(JsonSerializer.Serialize(response), mediaType.MediaType);
    }
}
