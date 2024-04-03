using System.Net.Http.Json;
using System.Text.Json;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace BuyDozerBeMain.Application.UserEntitys.Commands.LoginUserEntity;

public record LoginUserEntityCommand : IRequest<string>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public class LoginUserEntityCommandHandler : IRequestHandler<LoginUserEntityCommand, string>
{
    private readonly UserManager<UserEntity> _userManager;
    public LoginUserEntityCommandHandler(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(LoginUserEntityCommand request, CancellationToken cancellationToken)
    {
        var login = new UserEntityLogin
        {
            Email = request.Email,
            Password = request.Password,
            TwoFactorCode = "string",
            TwoFactorRecoveryCode = "string"
        };
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            var validCredentials = await _userManager.CheckPasswordAsync(user, request.Password);
            if (validCredentials == true)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(user, "administrator");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync("https://localhost:5001/api/Users/login", login);
                string result = response.Content.ReadAsStringAsync().Result;
                JObject jsonResult = JObject.Parse(result);
                jsonResult.Add("UserId", user?.Id);
                jsonResult.Add("UserName", user?.UserName);
                jsonResult.Add("Email", user?.Email);
                jsonResult.Add("Password", request.Password);
                jsonResult.Add("IsAdmin", isAdmin);
                var jsonString = jsonResult.ToString();
                return jsonString;
            }
            else
            {
                Response response = new Response
                {
                    Status = 404,
                    Message = "Not Found",
                    Data = "Password tidak cocok"
                };
                return JsonSerializer.Serialize(response);
            }
        }
        else
        {
            Response response = new Response
            {
                Status = 404,
                Message = "Not Found",
                Data = "Email tidak ditemukan"
            };
            return JsonSerializer.Serialize(response); ;
        }

    }
}