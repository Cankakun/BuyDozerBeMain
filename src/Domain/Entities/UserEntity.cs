using Microsoft.AspNetCore.Identity;

namespace BuyDozerBeMain.Domain.Entities;

public class UserEntity : IdentityUser
{
    public string? CompanyUser { get; set; }
    public string? PositionUser { get; set; }
}
