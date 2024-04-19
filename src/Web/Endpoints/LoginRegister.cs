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

public class LoginRegisters : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapPost(RegisterUserEntity, "Register")
            .MapPost(LoginUserEntity, "Login");
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

}
