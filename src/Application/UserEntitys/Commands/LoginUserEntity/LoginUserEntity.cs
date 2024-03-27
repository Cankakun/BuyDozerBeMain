using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BuyDozerBeMain.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
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
            return "User Tidak Ditemukan";
        }

    }
}