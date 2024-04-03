namespace BuyDozerBeMain.Domain.Entities;
public class UserEntityRole
{
    public required UserEntity User { get; set; }
    public required bool IsAdmin { get; set; }
}