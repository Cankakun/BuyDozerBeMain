namespace BuyDozerBeMain.Application.UserEntitys.Queries.GetUserEntity;
public class UserEntityVm
{
    public IReadOnlyCollection<UserEntityDTO> Data { get; init; } = Array.Empty<UserEntityDTO>();
}