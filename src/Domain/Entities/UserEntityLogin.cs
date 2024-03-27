namespace BuyDozerBeMain.Domain.Entities;

public class UserEntityLogin
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string TwoFactorCode { get; set; }
    public required string TwoFactorRecoveryCode { get; set; }

}
